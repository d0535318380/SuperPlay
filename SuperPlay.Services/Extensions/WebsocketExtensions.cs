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
        context.Socket.ThrowIfNull(nameof(context.Socket));
        
        using var stream = new MemoryStream();
        var socket = context.Socket;
        WebSocketReceiveResult result;
        
        do
        {
            var buffer = new byte[1024];
            
            result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
            
            stream.Write(buffer);
            
        } while (
            !result.EndOfMessage && 
            !token.IsCancellationRequested && 
            socket.State == WebSocketState.Open);
        
        var instance = MessagePack.MessagePackSerializer.Deserialize<GenericMessage>(stream);

        instance.ConnectionId = context.Id;
        
        return instance;
    }
    
    public static async Task SendAsync(this SocketConnection context, IBaseResponse response, CancellationToken token = default)
    {
        context.ThrowIfNull(nameof(context));
        context.Socket.ThrowIfNull(nameof(context.Socket));
        response.ThrowIfNull(nameof(response));
        
        var message = GenericMessage.Create(response);
        var buffer = MessagePack.MessagePackSerializer.Serialize(response);
        
        await context.Socket.SendAsync(buffer, WebSocketMessageType.Binary, true, token);
    }
  }