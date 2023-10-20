using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace SuperPlay.Data.Tests;

public class RepositoryTests
{
    private readonly ITestOutputHelper _testOutput;
    private readonly ILoggerFactory _logFactory;

    public RepositoryTests(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
        _logFactory = Divergic.Logging.Xunit.LogFactory.Create(testOutput);
    }

    [Fact]
    public async Task MigrateTest()
    {
        var context = await CreateAsync();

        context
            .Should()
            .NotBeNull();
    }

    [Fact]
    public async Task Repository_Add_Test()
    {
        var context = await CreateAsync();
        var repository = new GenericRepository<int, User>(context);
        var player = User.Create("Test", Guid.NewGuid().ToString());
        
        var result = await repository.AddAsync(player);
        
        
        result
            .Should()
            .BePositive();
    }


    private async Task<ApplicationDbContext> CreateAsync()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            @"Data source=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=SuperPlay;Trusted_Connection=True;");
        optionsBuilder.BuildAdapter();

        var dbContext = new ApplicationDbContext(optionsBuilder.Options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
        
        return dbContext;   
    }
}