using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Services.Extensions;

public class WebSocketClient : IDisposable
{
    private readonly IMessageFactory _factory;
    private readonly ILogger<WebSocketClient> _logger;
    private readonly ClientWebSocket _socket = new();
    private CancellationTokenSource _cts;
    private Task _listerTask;

    public Action<IBaseResponse> OnMessageReceived { get; set; }
    
    public WebSocketClient(IMessageFactory factory, ILogger<WebSocketClient> logger)
    {
        _factory = factory;
        _logger = logger;
    }
    
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        await _socket.ConnectAsync(uri, cancellationToken);
        
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _listerTask = StartListeningAsync(cancellationToken);
        
        _logger.LogInformation("Connected to {Uri}", uri);
    }

    private async Task StartListeningAsync(CancellationToken token)
    {
        _logger.LogInformation("Receiving messages...");
        
        while (_socket.State == WebSocketState.Open && !token.IsCancellationRequested)
        {
            var message = await _socket.ReceiveAsync(token);
            var response = _factory.FromResponse(message);

            OnMessageReceived?.Invoke(response);
            _logger.LogInformation("Received message {Message}", message);
        }
    }

    public async Task SendAsync(IBaseRequest request, CancellationToken cancellationToken)
    {
        await _socket.SendAsync(request, cancellationToken);
        
        _logger.LogInformation("Sent request {Request}", request);
    }

    public void Dispose()
    {
        _cts.Cancel();
        _socket.Dispose();
    }
}