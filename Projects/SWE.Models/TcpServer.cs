using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    public class TcpServer
    {
        private static Dictionary<string, Action<string, StreamWriter>> GetRoutes = new();
        private static Dictionary<string, Action<string, StreamWriter>> PostRoutes = new();
        private static Dictionary<string, Action<string, string, StreamWriter, string>> ProtectedRoutes = new();


        private static List<User> users = new List<User>();
        private static Dictionary<string, string> userSessions = new();

        public void Start(string host, int port)
        {
            InitRoutes();
            IPAddress ip;

            if (host == "localhost") ip = IPAddress.Any;
            else ip = IPAddress.Parse(host);

            TcpListener server = new TcpListener(ip, port);
            server.Start();

            Console.WriteLine($"Server started on {host}:{port}");

            while (true)
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

                char[] bodyChars = new char[contentLength];
                reader.Read(bodyChars, 0, contentLength);
                string body = new string(bodyChars);

                switch (method)
                {
                    case "GET":
                        if (GetRoutes.ContainsKey(path))
                        {
                            GetRoutes[path](path, writer);
                        }
                        break;
                    case "POST":
                        if (PostRoutes.ContainsKey(path))
                        {
                            PostRoutes[path](body, writer);
                        }
                        else if (ProtectedRoutes.ContainsKey(path))
                        {
                            ProtectedRoutes[path](body, authHeader, writer, path);
                        }
                        break;
                    default:
                        writer.WriteLine("HTTP/1.1 404 Not Found");
                        break;
                }
            }
            client.Close();
        }

        private static void SendResponse(StreamWriter writer, int Statuscode, string body)
        {
            writer.WriteLine($"HTTP/1.1 {Statuscode}");
            writer.WriteLine("Content-Type: application/json");
            writer.WriteLine($"Content-Length: {body.Length}");
            writer.WriteLine();
            writer.WriteLine(body);
        }

        private static (bool IsAuthenticated, string UserToken) IsAuthenticated(string authHeader)
        {
            string username = null;
            string password = null;

            if (string.IsNullOrEmpty(authHeader)) return (false, null);

            string token = authHeader.Split(' ')[1];
            if (userSessions.ContainsValue(token))
            {
                return (true, token);
            }
            return (false, null);
        }

        private static void HandleGetTest(string path, StreamWriter writer)
        {
            SendResponse(writer, 200, "{'message': 'Hello World'}");
        }

        private static void HandleLogin(string body, StreamWriter writer)
        {
            User user = JsonConvert.DeserializeObject<User>(body);
            User foundUser = users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (foundUser != null)
            {
                string token = Guid.NewGuid().ToString();
                userSessions.Add(foundUser.UserName, token);
                SendResponse(writer, 200, $"{{'token': '{token}'}}");
            }
            else
            {
                SendResponse(writer, 401, "{'message': 'Unauthorized'}");
            }
        }

        private static void HandleRegisterUser(string body, StreamWriter writer)
        {
            User user = JsonConvert.DeserializeObject<User>(body);
            User foundUser = users.FirstOrDefault(u => u.UserName == user.UserName);

            if (foundUser == null)
            {
                users.Add(user);
                SendResponse(writer, 200, "{'message': 'User created'}");
            }
            else
            {
                SendResponse(writer, 400, "{'message': 'User already exists'}");
            }
        }

        private static void HandleProtected(string body, string authHeader, StreamWriter writer, string path)
        {
            var authResult = IsAuthenticated(authHeader);
            bool isAuthenticated = authResult.IsAuthenticated;
            string userToken = authResult.UserToken;

            if (isAuthenticated)
            {
                SendResponse(writer, 200, "{'message': 'Protected data'}");
            }
            else
            {
                SendResponse(writer, 401, "{'message': 'Unauthorized'}");
            }
        }

        private static void HandleLogout(string body, string authHeader, StreamWriter writer)
        {
            var authResult = IsAuthenticated(authHeader);
            bool isAuthenticated = authResult.IsAuthenticated;
            string userToken = authResult.UserToken;

            if (isAuthenticated)
            {
                userSessions.Remove(userToken);
                SendResponse(writer, 200, "{'message': 'Logged out'}");
            }
            else
            {
                SendResponse(writer, 401, "{'message': 'Unauthorized'}");
            }
        }

        private static string GenerateSessionToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}