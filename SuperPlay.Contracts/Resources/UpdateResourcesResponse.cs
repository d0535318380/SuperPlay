namespace SuperPlay.Contracts.Login;

public class UpdateResourcesResponse
{
    public ICollection<ResourceItem> Resources { get; set; } = new List<ResourceItem>();
}