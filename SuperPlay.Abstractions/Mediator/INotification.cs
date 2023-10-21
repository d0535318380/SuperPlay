namespace SuperPlay.Abstractions.Mediator;

public interface INotification
{
}

public class NotificationBase : INotification, IHasId<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Message{ get; set; }
}