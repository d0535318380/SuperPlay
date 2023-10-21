using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Events;

public class UserConnectedEvent : NotificationBase
{
    public Guid UserId { get; set; }
    public string ConnectionId { get; set; }

    public static UserConnectedEvent Create(Guid userId, string? requestConnectionId)
        => new()
        {
            UserId = userId,
            ConnectionId = requestConnectionId ?? Guid.NewGuid().ToString()
        };
}

public class UserNotificationEvent : NotificationBase
{
    public Guid UserId { get; set; }
    public  INotification Payload { get; set; }

    public static UserNotificationEvent Create(Guid userId, INotification payload)
        => new()
        {
            UserId = userId,
            Payload = payload
        };
}