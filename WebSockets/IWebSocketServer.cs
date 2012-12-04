using System.Net.WebSockets;

namespace WebSocketListener
{
	public interface IWebSocketServer
	{
		string Protocol { get; }

		void HandleContext(HttpListenerWebSocketContext context);
	}
}
