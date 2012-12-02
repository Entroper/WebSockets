using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebSockets
{
	class HttpWebSocketListener
	{
		IHttpServer httpServer;
		IWebSocketServer webSocketServer;

		HttpListener listener;

		public bool IsRunning { get; private set; }

		public HttpWebSocketListener(IHttpServer httpServer, IWebSocketServer webSocketServer)
		{
			this.httpServer = httpServer;
			this.webSocketServer = webSocketServer;

			listener = new HttpListener();
			listener.Prefixes.Add("http://*:8080/");
		}

		public async void Run()
		{
			listener.Start();

			IsRunning = true;

			while (IsRunning)
			{
				var context = await listener.GetContextAsync();
				Task.Factory.StartNew(() => ContextHandler(context));
			}
		}

		public void Stop()
		{
			IsRunning = false;
			listener.Stop(); // Need to test whether this cancels GetContextAsync!
		}

		private async void ContextHandler(HttpListenerContext context)
		{
			var request = context.Request;
			Console.WriteLine("{0} {1} {2}", request.RemoteEndPoint.Address, request.HttpMethod, request.Url);

			if (request.IsWebSocketRequest)
				webSocketServer.HandleContext(await context.AcceptWebSocketAsync(webSocketServer.Protocol));
			else
				httpServer.HandleContext(context);
		}
	}
}
