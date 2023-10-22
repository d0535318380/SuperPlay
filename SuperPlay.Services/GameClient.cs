using System.Reactive.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Gift;
using SuperPlay.Contracts.Login;
using SuperPlay.Contracts.Resources;
using SuperPlay.Data.Configuration;

namespace SuperPlay.Services;

public class GameClient : BackgroundService
{
    private readonly ILogger<GameClient> _logger;
    private readonly WebSocketClient _webSocketClient;
    private readonly Uri _uri = new("ws://localhost:5080/ws");
    private IDisposable? _timer;
    private LoginResponse? _loginResponse;

    public GameClient(WebSocketClient webSocketClient, ILogger<GameClient> logger)
    {
        _webSocketClient = webSocketClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var loginRequest = new LoginRequest
        {
            Token = Guid.NewGuid().ToString(),
        };
        
        _webSocketClient.OnMessageReceived += OnMessageReceived;       
        
        await _webSocketClient.ConnectAsync(_uri, stoppingToken);
        await _webSocketClient.SendAsync(loginRequest, stoppingToken);

        _timer = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Subscribe(OnNext);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();
        _logger.LogInformation("GameClient stopped");
        
        return base.StopAsync(cancellationToken);
    }

    private void OnMessageReceived(IBaseResponse response)
    {
        switch (response)
        {
            case LoginResponse loginResponse:
                _loginResponse = loginResponse;
                _logger.LogInformation("LoginResponse: {Token}", _loginResponse.UserId);
                return;
            default:
                _logger.LogInformation("Response received: {Response}", response);
                return;
        }
    }

    private void OnNext(long _)
    {
        SendCommandsAsync().ConfigureAwait(false);
    }

    private async Task SendCommandsAsync()
    {
        if(_loginResponse is null)
        {
            return;
        }

        var updateCommand = new UpdateResourcesCommand()
        {
            UserId = _loginResponse.UserId,
            Item = new ResourceItem()
            {
                Key = 1,
                Value = 1000,
            }
        };

        var sendGiftCommand = new SendGiftCommand()
        {
            UserId = _loginResponse.UserId,
            ToUserId = UserConfig.DefaultFriend.Id,
            Item = new ResourceItem()
            {
                Key = 1,
                Value = 10,
            }
        };

        await _webSocketClient.SendAsync(updateCommand, CancellationToken.None);
        await _webSocketClient.SendAsync(sendGiftCommand, CancellationToken.None);
    }
}