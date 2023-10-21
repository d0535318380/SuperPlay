namespace SuperPlay.Abstractions.Mediator;

public interface IPublisher
{
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken token = default)
        where TNotification : INotification;
}