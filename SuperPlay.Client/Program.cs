using Serilog;
using Serilog.Formatting.Compact;

var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;
var  services = builder.Services;
builder.Services.AddLogging();

services.AddSerilog(cfg => 
    cfg.WriteTo.Async(wt => wt.Console(new RenderedCompactJsonFormatter())));


var host = builder.Build();
host.Run();