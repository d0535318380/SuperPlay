using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Login;

namespace SuperPlay.Handlers;

public sealed class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;

    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        // var predicate = new Predicate<User>(x => x.Tokens.Any(t => t.Token == request.Token));
        // var user = await _userRepository.GetAsync(request.Username, cancellationToken);
        //

        throw new NotImplementedException();
    }
}