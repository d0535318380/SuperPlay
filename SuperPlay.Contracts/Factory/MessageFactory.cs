using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Factory;

public class MessageFactory : IMessageFactory
{
    private readonly Dictionary<string, Type> _typesMap;
    private readonly ILogger<MessageFactory> _logger;

    public MessageFactory(ILogger<MessageFactory> logger)
    {
        _logger = logger;

        _typesMap = this.GetType().Assembly.GetTypes()
            .Where(x => 
                x.IsAssignableTo(typeof(IBaseRequest)) ||
                x.IsAssignableTo(typeof(IBaseResponse)))
            .ToDictionary(x => x.FullName ?? x.Name, x => x, StringComparer.OrdinalIgnoreCase);
    }

    public IBaseRequest FromRequest(GenericMessage message)
    {
        var instance = Deserialize(message) as IBaseRequest;

        if(instance is IHasConnectionId connection)
        {
            connection.ConnectionId = message.ConnectionId;
        }
        
        return instance ?? throw new InvalidOperationException($"Message {message.Type} is null");
    }
    
    
    public IBaseResponse FromResponse(GenericMessage message)
    {
        var instance = Deserialize(message) as IBaseResponse;
        
        return instance ?? throw new InvalidOperationException($"Message {message.Type} is null");
    }

    private object? Deserialize(GenericMessage message)
    {
        message.ThrowIfNull(nameof(message));

        if (!_typesMap.ContainsKey(message.Type))
        {
            _logger.LogError("Message type {MessageType} is not registered", message.Type);

            throw new ArgumentException($"Message type {message.Type} is not registered");
        }

        var type = _typesMap[message.Type];

        var instance = MessagePack.MessagePackSerializer.Deserialize(type, message.Payload);

        _logger.LogTrace("Message {MessageType} created", message.Type);

        return instance;
    }
}