using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SuperPlay.Data;
using Xunit.Abstractions;

namespace SuperPlay.Services.Tests;

public class RepositoryTests : TestBase
{
    public RepositoryTests(ITestOutputHelper testOutput) : base(testOutput)
    {
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
        var repository = new RepositoryGeneric<int, User>(context);
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