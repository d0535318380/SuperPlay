namespace SuperPlay.Contracts.Login;

public class UpdateResourcesCommand
{
    public Guid UserId { get; set; }
    public ICollection<ResourceItem> Resources { get; set; } = new List<ResourceItem>();
}