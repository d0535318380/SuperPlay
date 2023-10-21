using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Gift;

namespace SuperPlay.Contracts.Resources;

public class UpdateResourcesResponse : BaseResponse
{
    public Guid UserId { get; set; }
    public ResourceItem Item { get; set; }

    public static UpdateResourcesResponse Create(UserResource item) =>
        new()
        {
            UserId = item.UserId,
            Item = new ResourceItem()
            {
                Key = item.Key,
                Value = item.Value
            }
        };
}