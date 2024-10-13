// See https://aka.ms/new-console-template for more information
using SWE.Models;
using System.Net.Security;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.InteropServices.Marshalling;
using System.Reflection.Metadata;

class Program {
    static void Main(string[] args) 
    {
        TcpServer.Start("localhost", 10001);
    }

    class TcpServer
    {
        private Dictionary<string, <Action<string, StreamWriter>> GetRoutes = new ();
        private Dictionary<string, <Action<string, StreamWriter>> PostRoutes = new ();
        private Dictionary<string, <Action<string, string, StreamWriter, string>> ProtectedRoutes = new ();

        private List<User> users = new List<User>();
        private Dictionary<string, string> userSessions = new();

        public static void Start(string host, int port)
        {
            InitRoutes();
            IPAddress ip;

            if (host == "localhost") ip = IPAddress.Any;
            else ip = IPAddress.Parse(host);

            TcpListener server = new TcpListener(ip, port);
            server.Start();

            Console.WriteLine($"Server started on {host}:{port}");

            while(true)
            {
                TcpClient client = server.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        public static void InitRoutes() 
        {
            GetRoutes.Add("api/test", HandleGetTest);
            PostRoutes.Add("/sessions", HandleLogin);
            PostRoutes.Add("/users", HandleRegisterUser);
            ProtectedRoutes.Add("/api/protected", HandleProtected);
        }

        public static void HandleClient(TcpClient client)
        {
            using (NetworkStream networkStream = client.GetStream())
            using (StreamReader reader = new StreamReader(networkStream))
            using (StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true })
            {
                string requestLine = reader.ReadLine();
                if (string.IsNullOrEmpty(requestLine)) return;

                string[] requestParts = requestLine.Split(' ');
                if (requestParts.Length < 3) return;

                string method = requestParts[0];
                string path = requestParts[1];

                int contentLength = 0;
                string authHeader = null;
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    if (line.StartsWith("Content-Length:"))
                    {
                        contentLength = int.Parse(line.Split(' ')[1]);
                    }
                    else if (line.StartsWith("Authorization:"))
                    {
                        authHeader = line.Split(' ')[1];
                    }
                }

                if (method == "GET" && GetRoutes.ContainsKey(path))
                {
                    GetRoutes[path](path, writer);
                }
                else if (method == "POST" && PostRoutes.ContainsKey(path))
                {
                    string body = reader.ReadToEnd();
                    PostRoutes[path](body, writer);
                }
                else if (method == "POST" && ProtectedRoutes.ContainsKey(path))
                {
                    string body = reader.ReadToEnd();
                    ProtectedRoutes[path](body, authHeader, writer, path);
                }
                else
                {
                    writer.WriteLine("HTTP/1.1 404 Not Found");
                }
            }
}