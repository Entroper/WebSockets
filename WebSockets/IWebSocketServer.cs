using System.Net.WebSockets;

namespace WebSockets
{
	interface IWebSocketServer
	{
		string Protocol { get; }

		void HandleContext(HttpListenerWebSocketContext context);
	}
}
