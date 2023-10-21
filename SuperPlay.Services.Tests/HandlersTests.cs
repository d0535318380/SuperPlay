using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace SuperPlay.Services.Tests;

public class HandlersTests : TestBase
{
    public HandlersTests(ITestOutputHelper testOutput) : base(testOutput)
    {
    }

    [Fact]
    public void ServiceCollectionTests()
    {
        var services = new ServiceCollection();
        
    }
}