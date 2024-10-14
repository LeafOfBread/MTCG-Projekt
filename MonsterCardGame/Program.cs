using SWE.Models;
using Newtonsoft.Json;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.InteropServices.Marshalling;
using System.Reflection.Metadata;

        TcpServer tcpServer = new TcpServer();
        tcpServer.Start("localhost", 10001);