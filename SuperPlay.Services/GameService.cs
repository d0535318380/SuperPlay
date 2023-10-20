using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;
using SuperPlay.Contracts.Login;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Services.Extensions;

namespace SuperPlay.Services;

public class GameService : BackgroundService, 
    IGameService,
    INotificationHandler<LoginEvent>
{
    private readonly IMessageFactory _messageFactory;
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ILogger<GameService> _logger;
    
    private readonly ConcurrentDictionary<string, SocketConnection> _sockets = new();

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
   

    
    public void StartListenerAsync(SocketConnection connection, TaskCompletionSource<object> tcs)
    {
        connection.ThrowIfNull(nameof(connection));
        
        _sockets.TryAdd(connection.Id, connection);
        
        _logger.LogInformation("StartListenerAsync: {ConnectionId}", connection.Id);

        try
        {
            var token = new CancellationTokenSource();
            var task = HandleRequestAsync(connection, token.Token);
            
            tcs.SetResult(task);
        }
        catch (Exception ex)
        {
          tcs.SetException(ex);
           _logger.LogError(ex, "StartListenerAsync: {ConnectionId}", connection.Id);
        }
    }

    
    private async Task HandleRequestAsync(SocketConnection connection, CancellationToken token)
    {
        var socket = connection.Socket;

        while (socket.State == WebSocketState.Open && !token.IsCancellationRequested)
        {
            var message = await connection.ReceiveAsync(token);
            var request = _messageFactory.FromRequest(message);
            var response = await _mediator.SendAsync(request, token);

            await connection.SendAsync(response, token);
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(LoginEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

