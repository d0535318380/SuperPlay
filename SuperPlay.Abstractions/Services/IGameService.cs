using System.Net.WebSockets;
using SuperPlay.Abstractions.Extensions;

namespace SuperPlay.Abstractions.Services;

public class SocketConnection : IHasStringId
{
    public string Id { get; set; } = Guid.NewGuid().ToString("P");
    
    public Guid UserId { get; set; }
    public WebSocket Socket { get; set; }

    public TaskCompletionSource<object> TaskCompletionSource { get; set; } = new();
    
    public static SocketConnection Create(WebSocket socket)
        => new SocketConnection()
        {
            Socket = socket.ThrowIfNull(nameof(socket))
        };
}

public interface IGameService 
{ 
    Task StartListenerAsync(SocketConnection item, CancellationToken token);
}