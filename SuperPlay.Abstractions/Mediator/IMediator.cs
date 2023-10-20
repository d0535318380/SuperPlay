namespace SuperPlay.Abstractions.Mediator;


public interface IPublisher
{
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken token = default)
        where TNotification : INotification;
}

public interface ISender
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken token = default) 
        where TResponse : class;
}

public interface INotification
{
}

public interface IMediator : IPublisher, ISender
{
    
}

public interface IRequestHandler
{
    Task<TResponse> HandleAsync<TResponse>(IRequest<TResponse> request, CancellationToken token = default) 
        where TResponse : class;
}

/// <summary>
/// Defines a handler for a notification
/// </summary>
/// <typeparam name="TNotification">The type of notification being handled</typeparam>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    /// <summary>
    /// Handles a notification
    /// </summary>
    /// <param name="notification">The notification</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}


