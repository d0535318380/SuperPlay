using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Gift;

public class GiftEvent : NotificationBase
{
    public Guid UserId { get; set; }
    public Guid FromUserId { get; set; }
    public ResourceItem Item { get; set; }

    public static GiftEvent Create(Guid userId, UserResource item) =>
        new()
        {
            UserId = item.UserId,
            FromUserId = userId,
            Item = new ResourceItem()
            {
                Key = item.Key,
                Value = item.Value
            }
        };
}
