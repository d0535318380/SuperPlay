using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;
using SuperPlay.Contracts.Events;
using SuperPlay.Contracts.Factory;
using SuperPlay.Data;
using SuperPlay.Handlers;

namespace SuperPlay.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServerServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddSerilog(cfg => 
            cfg
                // .MinimumLevel.Information()
                .WriteTo.Async(wt => wt.Console()));

        services.AddDistributedMemoryCache();

        services.AddSingleton<GameService>();
        services.AddSingleton<IGameService>(sp => sp.GetRequiredService<GameService>());
        services.AddHostedService<GameService>(sp => sp.GetRequiredService<GameService>());

        services.AddHandlers();
        
        services.AddSingleton<IMessageFactory, MessageFactory>();
        services.AddSingleton<IMediator, Mediator>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IResourceRepository, UserResourceRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan
                .FromAssemblyOf<LoginHandler>()
                .AddClasses()
                .AsImplementedInterfaces());

        services.AddSingleton<INotificationHandler<UserConnectedEvent>, GameService>(sp => sp.GetRequiredService<GameService>());
        services.AddSingleton<INotificationHandler<UserNotificationEvent>, GameService>(sp => sp.GetRequiredService<GameService>());
        services.AddSingleton<IRequestHandler<GenericMessage, IBaseResponse>, GameService>(sp => sp.GetRequiredService<GameService>());

        return services;
    }
}