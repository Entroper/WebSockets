using System.Net;
using System.Text;

namespace WebSockets
{
	class HelloWorldServer : IHttpServer
	{
		public async void HandleContext(HttpListenerContext context)
		{
			var response = context.Response;
			response.ContentType = "text/html";
			byte[] output = Encoding.UTF8.GetBytes("Hello world!");
			await response.OutputStream.WriteAsync(output, 0, output.Length);
			response.Close();
		}
	}
}
