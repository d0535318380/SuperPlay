using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;
using SuperPlay.Abstractions.Services;
using SuperPlay.Data;
using SuperPlay.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


services.AddHostedService<GameService>();
services.AddSingleton<IGameService, GameService>(sp => sp.GetRequiredService<GameService>());

services.AddSerilog(cfg => 
    cfg.WriteTo.Async(wt => wt.Console(new RenderedCompactJsonFormatter())));

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
    var connection = SocketConnection.Create(webSocket);
    
    await gameService.StartListenerAsync(connection, token);
});


app.Run();