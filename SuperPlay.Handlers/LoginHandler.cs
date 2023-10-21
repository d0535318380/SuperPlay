using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Events;
using SuperPlay.Contracts.Extensions;
using SuperPlay.Contracts.Login;

namespace SuperPlay.Handlers;

public sealed class LoginHandler : RequestHandlerBase<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public LoginHandler(
        IUserRepository  userRepository, 
        IMediator mediator,
        IDistributedCache cache, 
        ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _cache = cache;
    }
    
    protected override async Task<LoginResponse> HandleInternalAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await GetOrCreateUserAsync(request, cancellationToken);
        var isUserConnected = await _cache.IsUserConnectedAsync(user.Id, cancellationToken);
        var notification = UserConnectedEvent.Create(user.Id, request.ConnectionId);
        var response = new LoginResponse()
        {
            UserId = user.Id,
            Status = isUserConnected ? RequestStatus.Failed : RequestStatus.Success
        };

        if (isUserConnected)
        {
            response.Message = "User is already connected";
            return response;
        }

        await _cache.SetUserConnectionAsync(request, cancellationToken);
        await _mediator.PublishAsync(notification, cancellationToken);
        
        return response;
    }

    private async Task<User> GetOrCreateUserAsync(LoginRequest request, CancellationToken cancellationToken)
    {
      var user = await _userRepository.FindAsync(
            x=>x.Tokens.Any(t=>t.Token == request.Token), cancellationToken);

      if (user is not null)
      {
          return user;
      }
    
      user = User.Create($"Guest-{request.Token}", request.Token);
      
      await _userRepository.AddAsync(user, cancellationToken);

      return user;
    }
}


          