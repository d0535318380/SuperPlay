using FluentAssertions;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Factory;
using SuperPlay.Contracts.Factory;
using SuperPlay.Contracts.Login;
using Xunit.Abstractions;

namespace SuperPlay.Contracts.Tests;

public class MessageFactoryTests
{
    private readonly ITestOutputHelper _testOutput;
    private readonly ILoggerFactory _logFactory;

    public MessageFactoryTests(ITestOutputHelper  testOutput)
    {
        _testOutput = testOutput;
        _logFactory = Divergic.Logging.Xunit.LogFactory.Create(testOutput);
    }
    
    [Fact]
    public void CreateMessage_Success_Test()
    {
        var source = new LoginRequest();
        var message = GenericMessage.Create(source);
        var factory = Create();
        
        var result = factory.FromRequest(message);
        
        result
            .Should()
            .NotBeNull()
            .And
            .BeOfType<LoginRequest>();
    }



    private IMessageFactory Create()
        => new MessageFactory(_logFactory.CreateLogger<MessageFactory>());

}