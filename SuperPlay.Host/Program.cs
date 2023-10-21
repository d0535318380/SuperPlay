using SuperPlay.Abstractions.Services;
using SuperPlay.Services;
using SuperPlay.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddServerServices(builder.Configuration);

var app = builder.Build();

app.UseWebSockets();


app.MapGet("/ws", async (HttpContext httpContext, IGameService gameService, CancellationToken token) =>
{
    if (!httpContext.WebSockets.IsWebSocketRequest)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }
    
    using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
    var connection = SocketConnection.Create(webSocket);
    
    await gameService.StartListenerAsync(connection, token);
});


app.Run();