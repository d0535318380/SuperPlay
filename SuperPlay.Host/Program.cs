using SuperPlay.Abstractions.Services;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseWebSockets();


app.MapGet("/ws", async (IGameService gameService, HttpContext httpContext, CancellationToken token) =>

{
    if (!httpContext.WebSockets.IsWebSocketRequest)
    {
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }
    
    using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
    var socketFinishedTcs = new TaskCompletionSource<object>();
    var connection = SocketConnection.Create(webSocket);
    
    gameService.StartListenerAsync(connection, socketFinishedTcs);
    
    await socketFinishedTcs.Task;
});


app.Run();