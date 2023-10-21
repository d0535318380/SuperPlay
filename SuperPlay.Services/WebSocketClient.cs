using System.Net.WebSockets;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Services.Extensions;

public class WebSocketClient : IDisposable
{
    private readonly ILogger<WebSocketClient> _logger;
    private readonly ClientWebSocket _webSocket = new();
    
    public WebSocketClient(ILogger<WebSocketClient> logger)
    {
        _logger = logger;
    }
    
    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        await _webSocket.ConnectAsync(uri, cancellationToken);
        
        _logger.LogInformation("Connected to {Uri}", uri);
        
        _logger.LogInformation("Receiving messages...");
    }

    public async Task SendAsync(IBaseRequest request, CancellationToken cancellationToken)
    {
        await _webSocket.SendAsync(request, cancellationToken);
        
        _logger.LogInformation("Sent request {Request}", request);
    }

    public void Dispose()
    {
        _webSocket.Dispose();
    }
}