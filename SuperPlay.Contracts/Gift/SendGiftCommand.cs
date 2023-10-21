using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Gift;

public class SendGiftCommand : BaseRequest<SendGiftResponse>
{
    public Guid UserId { get; set; }
    public Guid ToUserId { get; set; }
    public ResourceItem Item { get; set; }
}