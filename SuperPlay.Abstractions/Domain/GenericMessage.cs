using MessagePack;
using SuperPlay.Abstractions.Extensions;

namespace SuperPlay.Abstractions.Domain;

[MessagePackObject(keyAsPropertyName: true)]
public class GenericMessage : IHasStringId, IHasConnectionId
{
    public string Id { get; set; }
    public string Type { get; set; }

    public string? ConnectionId { get; set; }
    public byte[] Payload { get; set; }
    
    public static GenericMessage Create(IHasStringId payload) 
    {
        payload.ThrowIfNull(nameof(payload));
        
        var type = payload.GetType();
        
        return new GenericMessage
        {
            Id = payload.Id,
            Type = type.FullName ?? type.Name,
            Payload = MessagePackSerializer.Serialize((object) payload)
        };
    }
}

