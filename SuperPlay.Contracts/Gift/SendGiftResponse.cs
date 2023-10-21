using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Gift;

public class SendGiftResponse : BaseResponse
{
    public Guid UserId { get; set; }
    public ResourceItem Item { get; set; }

    public static SendGiftResponse Create(UserResource source)
        => new() { UserId = source.UserId, Item = new ResourceItem()
        {
            Key = source.Key,
            Value = source.Value
        } };
}