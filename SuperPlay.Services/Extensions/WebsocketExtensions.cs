using System.Net.WebSockets;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;

namespace SuperPlay.Services.Extensions;

public static class WebSocketExtensions
{
    public static async Task<GenericMessage> ReceiveAsync(this SocketConnection context, CancellationToken token = default)
    {
        context.ThrowIfNull(nameof(context));
        
        var instance = await context.Socket.ReceiveAsync(token);
        instance.ConnectionId = context.Id;

        return instance;
    }
    
    public static async Task<GenericMessage> ReceiveAsync(this WebSocket context, CancellationToken token = default)
    {
        context.ThrowIfNull(nameof(context));
        
        using var stream = new MemoryStream();
        WebSocketReceiveResult result;
        
        do
        {
            var buffer = new byte[1024];
            
            result = await context.ReceiveAsync(new ArraySegment<byte>(buffer), token);
            
            stream.Write(buffer);
            
        } while (
            !result.EndOfMessage && 
            !token.IsCancellationRequested && 
            context.State == WebSocketState.Open);
        
        var instance = MessagePack.MessagePackSerializer.Deserialize<GenericMessage>(stream);


        
        return instance;
    }

    public static async Task SendAsync(this SocketConnection context, IHasStringId message, CancellationToken token = default)
    {
        context.ThrowIfNull(nameof(context));
        message.ThrowIfNull(nameof(message));
        
        await context.Socket.SendAsync(message, token);
    }

    public static async Task SendAsync(this WebSocket context, IHasStringId item, CancellationToken token = default)
    {
        context.ThrowIfNull(nameof(context));
        item.ThrowIfNull(nameof(item));
        
        var message = GenericMessage.Create(item);
        var buffer = MessagePack.MessagePackSerializer.Serialize(message, cancellationToken: token);
        
        await context.SendAsync(buffer, WebSocketMessageType.Binary, true, token);
    }
  }