using SWE.Models;
using Newtonsoft.Json;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.InteropServices.Marshalling;
using System.Reflection.Metadata;

//*link zu github repository*: https://github.com/LeafOfBread/MTCG-Projekt/tree/master

/*user classes, cards etc. sind basically platzhalter und somit incomplete, methoden 
innerhalb der klassen sind deprecated und werden erst fuer die volle abgabe vervollständigt,
relevant ist somit also nur die tcpserver klasse*/

        TcpServer tcpServer = new TcpServer();
        tcpServer.Start("localhost", 10001);