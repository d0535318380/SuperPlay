using Serilog;
using Serilog.Formatting.Compact;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Contracts.Factory;
using SuperPlay.Services;

var builder = Host.CreateApplicationBuilder(args);
var  services = builder.Services;


services
    .AddSerilog(cfg => 
    cfg.WriteTo.Async(wt => wt.Console()));

services
    .AddSingleton<IMessageFactory,MessageFactory>()
    .AddTransient<WebSocketClient>()
    .AddHostedService<GameClient>();




var host = builder.Build();
host.Run();