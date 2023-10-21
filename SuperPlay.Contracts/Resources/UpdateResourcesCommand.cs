using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Gift;
using SuperPlay.Contracts.Resources;

namespace SuperPlay.Contracts.Login;

public class UpdateResourcesCommand : BaseRequest<UpdateResourcesResponse>
{
    public Guid UserId { get; set; }
    public ResourceItem Item { get; set; } 
}