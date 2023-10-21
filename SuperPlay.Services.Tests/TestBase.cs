using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperPlay.Data;
using SuperPlay.Data.Configuration;
using SuperPlay.Services.Extensions;
using Xunit.Abstractions;

namespace SuperPlay.Services.Tests;

public abstract class  TestBase
{
    protected readonly ILoggerFactory LogFactory;

    protected TestBase(ITestOutputHelper testOutput)
    {
        LogFactory = Divergic.Logging.Xunit.LogFactory.Create(testOutput);
    }
    
    protected T CreateInstance<T>()
    {
        var type = typeof(T);
        var instance = CreateInstance(type);

        return (T)instance;
    }
    
    protected object CreateInstance(Type type)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddServerServices(config);
        serviceCollection.AddLogging();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var instance = serviceProvider.GetRequiredService(type);
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Set<User>()
            .AddRange(UserConfig.DefaultUser, UserConfig.DefaultFriend);
        
        dbContext.SaveChanges();
        dbContext.Dispose();
        return instance;
    }
}