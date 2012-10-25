using System.Net;

namespace WebSockets
{
	interface IHttpServer
	{
		void HandleContext(HttpListenerContext context);
	}
}
