using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Resources;

public class UpdateResourcesCommand : BaseRequest<UpdateResourcesResponse>
{
    public Guid UserId { get; set; }
    public ResourceItem Item { get; set; } 
}