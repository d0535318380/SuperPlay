using System.Dynamic;
using System.Net.WebSockets;
using SuperPlay.Abstractions.Extensions;

namespace SuperPlay.Abstractions.Services;

public class SocketConnection : IHasId
{
    public string Id { get; set; } = Guid.NewGuid().ToString("P");
    public WebSocket Socket { get; set; }

    public static SocketConnection Create(WebSocket socket)
        => new SocketConnection()
        {
            Socket = socket.ThrowIfNull(nameof(socket))
        };
}

public interface IGameService 
{ 
    void StartListenerAsync(SocketConnection item, TaskCompletionSource<object> tcs);
}