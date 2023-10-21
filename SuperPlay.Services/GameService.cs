using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Contracts.Events;
using SuperPlay.Services.Extensions;

namespace SuperPlay.Services;

public sealed class GameService : BackgroundService, 
    IGameService,
    INotificationHandler<UserConnectedEvent>,
    INotificationHandler<UserNotificationEvent>,
    IRequestHandler<GenericMessage, IBaseResponse>
{
    private readonly IMessageFactory _messageFactory;
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ILogger<GameService> _logger;
    
    private readonly ConcurrentDictionary<string, SocketConnection> _sockets = new();
    private readonly ConcurrentDictionary<Guid, SocketConnection> _userConnections = new();

    public GameService(
        IMessageFactory messageFactory,
        IMediator mediator,
        IDistributedCache cache,
        
        ILogger<GameService> logger)
    {
        _messageFactory = messageFactory.ThrowIfNull(nameof(messageFactory));;;
        _mediator = mediator.ThrowIfNull(nameof(mediator));
        _cache = cache.ThrowIfNull(nameof(cache));
        _logger = logger.ThrowIfNull(nameof(cache));;
    }    
   
    
    public async Task StartListenerAsync(SocketConnection connection, CancellationToken token)
    {
        connection.ThrowIfNull(nameof(connection));
        
        _sockets.TryAdd(connection.Id, connection);
        
        _logger.LogInformation("StartListenerAsync: {ConnectionId}", connection.Id);

        try
        {
            await HandleRequestAsync(connection, token);
            
        }
        catch (Exception ex)
        {
           _logger.LogError(ex, "StartListenerAsync: {ConnectionId}", connection.Id);
           throw;
        }
    }

    
    private async Task HandleRequestAsync(SocketConnection connection, CancellationToken token)
    {
        var socket = connection.Socket;

        while (socket.State == WebSocketState.Open && !token.IsCancellationRequested)
        {
            var message = await connection.ReceiveAsync(token);
            var response = await HandleAsync(message, token);

            await connection.SendAsync(response, token);
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public Task HandleAsync(UserConnectedEvent notification, CancellationToken cancellationToken = default)
    {
        var isExists = _sockets.TryGetValue(notification.ConnectionId, out var socket);

        if (!isExists || socket == null)
        {
            return Task.CompletedTask;
        }
        
        socket.UserId = notification.UserId;
        
        _userConnections.AddOrUpdate(notification.UserId, socket, (_, _) => socket);
        
        return Task.CompletedTask;
    }

    public async Task HandleAsync(UserNotificationEvent notification, CancellationToken token)
    {
        var isExists = _userConnections.TryGetValue(notification.UserId, out var connection);

        if (!isExists || connection == null)
        {
            return;
        }
        
        await connection.SendAsync(notification.Payload, token);
    }

    public async Task<IBaseResponse> HandleAsync(GenericMessage message, CancellationToken token = default)
    {
        try
        {
            var request = _messageFactory.FromRequest(message);
            var response = await _mediator.SendAsync(request, token);

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}

