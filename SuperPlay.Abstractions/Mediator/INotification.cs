namespace SuperPlay.Abstractions.Mediator;

public interface INotification : IHasStringId
{
}

public class NotificationBase : INotification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Message{ get; set; }
}