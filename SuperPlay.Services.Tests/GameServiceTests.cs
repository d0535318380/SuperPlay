using FluentAssertions;
using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Abstractions.Services;
using SuperPlay.Contracts.Events;
using SuperPlay.Contracts.Gift;
using SuperPlay.Contracts.Login;
using SuperPlay.Contracts.Resources;
using Xunit.Abstractions;

namespace SuperPlay.Services.Tests;

public class GameServiceTests : TestBase
{
    public GameServiceTests(ITestOutputHelper testOutput) : base(testOutput)
    {
    }

    [Theory]
    [InlineData(typeof(IGameService))]
    [InlineData(typeof(IMediator))]
    [InlineData(typeof(INotificationHandler<UserConnectedEvent>))]
    [InlineData(typeof(INotificationHandler<UserNotificationEvent>))]
    [InlineData(typeof(IRequestHandler<GenericMessage, IBaseResponse>))]
    [InlineData(typeof(IRequestHandler<LoginRequest, LoginResponse>))]
    [InlineData(typeof(IRequestHandler<UpdateResourcesCommand, UpdateResourcesResponse>))]
    [InlineData(typeof(IRequestHandler<SendGiftCommand, SendGiftResponse>))]
    public void CreateInstanceTest(Type type)
    {
        var instance = CreateInstance(type);

        instance
            .Should()
            .NotBeNull();
    }


    [Fact]
    public async Task Login_Handle_Test()
    {
        var request = new LoginRequest
        {
            Token = Guid.NewGuid().ToString(),
            ConnectionId = Guid.NewGuid().ToString()
        };
        var message = GenericMessage.Create(request);
        var instance = CreateInstance<GameService>();
        var result = await instance.HandleAsync(message);

        result
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public async Task SendGift_Handle_Test()
    {
        var instance = CreateInstance<GameService>();
        var request = new LoginRequest
        {
            Token = Guid.NewGuid().ToString(),
            ConnectionId = Guid.NewGuid().ToString()
        };
        var message = GenericMessage.Create(request);
        
        var result = await instance.HandleAsync(message);

        result
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public async Task UpdateResource_Handle_Test()
    {
        var request = new LoginRequest
        {
            Token = Guid.NewGuid().ToString(),
            ConnectionId = Guid.NewGuid().ToString()
        };
        var message = GenericMessage.Create(request);
        var instance = CreateInstance<GameService>();
        var result = await instance.HandleAsync(message);

        result
            .Should()
            .NotBeNull();
    }

}