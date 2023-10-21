using Microsoft.Extensions.DependencyInjection;

namespace SuperPlay.Handlers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan
                .FromAssemblyOf<LoginHandler>()
                .AddClasses());


        return services;
    }
}