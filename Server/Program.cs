using System.Net;
using System.Net.Sockets;
using System.Text;


var ip = IPAddress.Loopback;
var port = 13000;
var localEndPoint = new IPEndPoint(ip, port);

using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    socket.Bind(localEndPoint);
    socket.Listen(10);

    Console.WriteLine($"Server listening on {ip}:{port}");
    var handler = socket.Accept();

    var bytes = new byte[1024];
    var bytesRec = handler.Receive(bytes);
    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
    Console.WriteLine($"Received: {data}");

    var msg = Encoding.ASCII.GetBytes("Message received.");
    handler.Send(msg);
    Thread.Sleep(100000);

    handler.Shutdown(SocketShutdown.Both);
    handler.Close();
}