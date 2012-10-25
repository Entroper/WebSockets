namespace WebSockets
{
	class Program
	{
		static void Main(string[] args)
		{
			var httpServer = new HelloWorldServer();
			var webSocketServer = new EchoSocket();
			var listener = new HttpWebSocketListener(httpServer, webSocketServer);
			listener.Run();
		}
	}
}
