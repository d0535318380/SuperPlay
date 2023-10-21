using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Events;

public class UserConnectedEvent : NotificationBase
{
    public Guid UserId { get; set; }
    public string ConnectionId { get; set; }
}

public class UserNotificationEvent : NotificationBase
{
    public Guid UserId { get; set; }
    public string Type { get; set; }
    public byte[] Payload { get; set; }
}