using System.Net;

namespace WebSocketListener
{
	public interface IHttpServer
	{
		void HandleContext(HttpListenerContext context);
	}
}
