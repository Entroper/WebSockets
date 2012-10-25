using System;
using System.Net.WebSockets;
using System.Threading;

namespace WebSockets
{
	class EchoSocket : IWebSocketServer
	{
		public string Protocol
		{
			get { return "echo"; }
		}

		public async void HandleContext(HttpListenerWebSocketContext context)
		{
			var socket = context.WebSocket;

			while (socket.State == WebSocketState.Open)
			{
				var buffer = new ArraySegment<byte>(new byte[1024]);
				try
				{
					var result = await socket.ReceiveAsync(buffer, new CancellationToken());
					var outputBuffer = new ArraySegment<byte>(buffer.Array, 0, result.Count);
					await socket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, new CancellationToken());
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(String.Format("Web Socket exception: {0}", ex));
				}
			}
		}
	}
}
