using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;
using SuperPlay.Contracts.Login;
using System.Collections.Concurrent;

namespace SuperPlay.Services;

public class GameService : BackgroundService, 
    IGameService,
    INotificationHandler<LoginEvent>
{
    private readonly ConcurrentDictionary<string, SocketConnection> _sockets = new();
    private readonly IDistributedCache _cache;
    private readonly ILogger<GameService> _logger;

    public GameService(
        IDistributedCache cache,
        
        ILogger<GameService> logger)
    {
        _cache = cache.ThrowIfNull(nameof(cache));
        _logger = logger.ThrowIfNull(nameof(cache));;
    }    
   

    
    public void StartListenerAsync(SocketConnection connection, TaskCompletionSource<object> tcs)
    {
        connection.ThrowIfNull(nameof(connection));
        
        _sockets.TryAdd(connection.Id, connection);
        
        _logger.LogInformation("StartListenerAsync: {ConnectionId}", connection.Id);
        throw new NotImplementedException();
    }
    
  
    public Task HandleAsync(LoginEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }

}

