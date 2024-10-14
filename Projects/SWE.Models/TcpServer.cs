using System;
using System.Text.Json;
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
    //definiert route mit method, path und handler
    public class Route
    {
        public string Method { get; }
        public string Path { get; }
        public Func<string, StreamWriter, Task> Handler { get; }

        public Route(string method, string path, Func<string, StreamWriter, Task> handler)
        {
            Method = method;
            Path = path;
            Handler = handler;
        }
    }

    //definiert router mit routes und methoden zum registrieren und handlen von routes
    public class Router
    {
        private List<Route> routes = new List<Route>();

        public void RegisterRoute(string method, string path, Func<string, StreamWriter, Task> handler)
        {
            routes.Add(new Route(method, path, handler));
        }

        public async Task HandleRequest(string method, string path, StreamWriter writer, string body)
        {
            Route route = routes.FirstOrDefault(r => r.Method == method && r.Path == path);
            if (route != null)
            {
                await route.Handler(body, writer);
            }
            else
            {
                await writer.WriteLineAsync("HTTP/1.1 404 Not Found");
            }
        }
    }
    //tcp server mit users, sessions und router
    public class TcpServer
    {
        private static List<User> users = new List<User>(); //user list
        private static Dictionary<string, string> userSessions = new(); //dictionary fuer user sessions
        private static Router router = new Router();

        public void Start(string host, int port) //entry point
        {
            InitRoutes(); //initialisiert routes
            IPAddress ip = host == "localhost" ? IPAddress.Any : IPAddress.Parse(host); //setzt ip auf localhost oder host

            TcpListener server = new TcpListener(ip, port);
            server.Start();

            Console.WriteLine($"Server started on {host}:{port}");

            while (true) //solange server laeuft, akzeptiere clients
            {
                TcpClient client = server.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }

        public static void InitRoutes() // initialisiert routes
        {
            router.RegisterRoute("GET", "/api/test", async (body, writer) => HandleGetTest(body, writer));
            router.RegisterRoute("POST", "/sessions", async (body, writer) => HandleLogin(body, writer));
            router.RegisterRoute("POST", "/users", async (body, writer) => HandleRegisterUser(body, writer));

            //protected route
            router.RegisterRoute("POST", "/api/protected", async (body, writer) =>
            {
                string authHeader = body;
                HandleProtected(body, authHeader, writer, "api/protected");
            });
        }

        public static void HandleClient(TcpClient client)
        {
            using (NetworkStream networkStream = client.GetStream()) //liest request und schreibt response
            using (StreamReader reader = new StreamReader(networkStream))
            using (StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true })
            {
                string requestLine = reader.ReadLine();
                if (string.IsNullOrEmpty(requestLine)) return; //falls request leer, return

                string[] requestParts = requestLine.Split(' ');
                if (requestParts.Length < 3) return;

                string method = requestParts[0];
                string path = requestParts[1];

                int contentLength = 0;
                string authHeader = null;
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine())) //liest header
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

                Task.Run(() => router.HandleRequest(method, path, writer, body)).Wait(); //handelt request
            }
            client.Close();
        }

        private static void SendResponse(StreamWriter writer, int Statuscode, string body) //sendet response
        {
            writer.WriteLine($"HTTP/1.1 {Statuscode}");
            writer.WriteLine("Content-Type: application/json");
            writer.WriteLine($"Content-Length: {body.Length}");
            writer.WriteLine();
            writer.WriteLine(body);
        }

        private static (bool IsAuthenticated, string UserToken) IsAuthenticated(string authHeader) //prueft ob user authentifiziert ist + returned bool und token
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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            User user = JsonSerializer.Deserialize<User>(body, options);
            User foundUser = users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (foundUser != null) // checks if user exists
            {
                string token = Guid.NewGuid().ToString();
                userSessions.Add(foundUser.UserName, token);
                SendResponse(writer, 200, $"{{\"token\": \"{token}\"}}");
            }
            else
            {
                SendResponse(writer, 402, "{\"message\": \"Unauthorized\"}");
            }
        }

        private static void HandleRegisterUser(string body, StreamWriter writer)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            User user = JsonSerializer.Deserialize<User>(body, options);
            User foundUser = users.FirstOrDefault(u => u.UserName == user.UserName);

            if (foundUser == null) //prueft ob user existiert und added gegenfalls
            {
                users.Add(user);
                SendResponse(writer, 201, "{'message': 'User created'}");
            }
            else
            {
                SendResponse(writer, 401, "{'message': 'User already exists'}");
            }
        }

        private static void HandleProtected(string body, string authHeader, StreamWriter writer, string path)
        {
            var authResult = IsAuthenticated(authHeader);
            bool isAuthenticated = authResult.IsAuthenticated;
            string userToken = authResult.UserToken;

            if (isAuthenticated) //prueft ob user authentifiziert ist
            {
                SendResponse(writer, 201, "{'message': 'Protected data'}");
            }
            else
            {
                SendResponse(writer, 400, "{'message': 'Unauthorized'}");
            }
        }

        private static void HandleLogout(string body, string authHeader, StreamWriter writer) //handlet logout
        {
            var authResult = IsAuthenticated(authHeader);
            bool isAuthenticated = authResult.IsAuthenticated;
            string userToken = authResult.UserToken;

            if (isAuthenticated) //prueft ob user authentifiziert ist
            {
                userSessions.Remove(userToken); //loescht session token
                SendResponse(writer, 201, "{'message': 'Logged out'}");
            }
            else
            {
                SendResponse(writer, 401, "{'message': 'Unauthorized'}");
            }
        }

        private static string GenerateSessionToken() //generiert session token
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