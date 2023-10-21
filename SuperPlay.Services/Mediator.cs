using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Gift;
using SuperPlay.Contracts.Login;
using SuperPlay.Contracts.Resources;
using SuperPlay.Handlers;
using SuperPlay.Services.Extensions;

namespace SuperPlay.Services;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<Type, Type> _requestHandlers;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _requestHandlers = new ConcurrentDictionary<Type, Type>(GetHandlers());
    }

    public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken token = default)
        where TNotification : INotification
    {
        var handler = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

        var tasks = handler.Select(x => x.HandleAsync(notification, token));

        await Task.WhenAll(tasks);
    }

    public async Task<IBaseResponse> SendAsync(IBaseRequest request, CancellationToken token = default)
    {
        var requestType = request.GetType();
        var isExists = _requestHandlers.TryGetValue(requestType, out var handlerType);

        if (!isExists || handlerType == null)
        {
            throw new KeyNotFoundException($"Handler for request {requestType} not found");
        }

        var handler = _serviceProvider.GetService(handlerType);
        var response = await handler.InvokeHandleAsync(handlerType, request, token);


        return response;
    }


    private IEnumerable<KeyValuePair<Type, Type>> GetHandlers()
    {
        yield return new KeyValuePair<Type, Type>(typeof(LoginRequest), typeof(IRequestHandler<LoginRequest, LoginResponse>));
        yield return new KeyValuePair<Type, Type>(typeof(UpdateResourcesCommand), typeof(IRequestHandler<UpdateResourcesCommand,UpdateResourcesResponse>));
        yield return new KeyValuePair<Type, Type>(typeof(SendGiftCommand), typeof(IRequestHandler<SendGiftCommand, SendGiftResponse>));
    }
}