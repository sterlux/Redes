using System.Net;
using System.Net.Sockets;
using System.Text;

var ip = IPAddress.Loopback;
var port = 13000;

var remoteEP = new IPEndPoint(ip, port);

using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    socket.Connect(remoteEP);
    Console.WriteLine($"Socket connected to {socket.RemoteEndPoint.ToString()}");

    var msg = Encoding.ASCII.GetBytes("This is a test");
    int bytesSent = socket.Send(msg);

    var bytes = new byte[1024];
    int bytesRec = socket.Receive(bytes);
    Console.WriteLine($"Echoed test = {Encoding.ASCII.GetString(bytes, 0, bytesRec)}");

    Thread.Sleep(100000);
    
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}