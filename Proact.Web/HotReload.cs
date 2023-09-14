using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using Proact.Web;
using ProactSandbox.Controllers;

[assembly: MetadataUpdateHandler(typeof(HotReload))]

namespace Proact.Web
{
    internal static class HotReload
    {
        public static void UpdateApplication( Type[]? _ )
        {
            Console.WriteLine("HI");
            WebSocketController.webSocket?.SendAsync(CreateMessage(), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    
        private static ArraySegment<byte> CreateMessage()
        {
            var messageString = JsonSerializer.Serialize(WebSocketMessage.CreateHotReload());
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageString));
        }
    }
}

