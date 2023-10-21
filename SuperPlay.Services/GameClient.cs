using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SuperPlay.Services;

public class GameClient : BackgroundService
{
    private readonly ILogger<GameClient> _logger;

    public GameClient(ILogger<GameClient> logger)
    {
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}