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

		public void Run()
		{
			listener.Start();

			IsRunning = true;

			while (IsRunning)
			{
				var context = listener.GetContext();
				Task.Factory.StartNew(() => ContextHandler(context));
			}
		}

		public void Stop()
		{
			IsRunning = false;
		}

		private async void ContextHandler(HttpListenerContext context)
		{
			if (IsWebSocketRequest(context.Request))
				webSocketServer.HandleContext(await context.AcceptWebSocketAsync(webSocketServer.Protocol));
			else
				httpServer.HandleContext(context);
		}

		private static bool IsWebSocketRequest(HttpListenerRequest request)
		{
			var scheme = request.Url.Scheme;
			var upgradeValues = request.Headers.GetValues("Upgrade");
			var connectionValues = request.Headers.GetValues("Connection");

			return (scheme == "ws" || scheme == "wss") && connectionValues.Contains("Upgrade") && upgradeValues.Contains("websocket");
		}
	}
}
