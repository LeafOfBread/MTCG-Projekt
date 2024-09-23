using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    class SimpleHTTPServer
    {
        public static void main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, 8080);

                server.Start();
                Console.WriteLine("Server started on Port 8080");

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() => HandleRequest(client));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server?.Stop();
            }
        }
        public static void HandleRequest(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string Request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received Request: " + $"{Request}");

                if (Request.StartsWith("GET"))
                {
                    string response = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello World!";
                    byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseBuffer, 0, responseBuffer.Length);
                }

                client.Close();

            }


            catch (Exception e)
            {
                Console.Write("Error: " + $"{e.Message}");
            }
        }
    }
}