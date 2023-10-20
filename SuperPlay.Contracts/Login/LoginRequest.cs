using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Login;

public class LoginRequest : RequestBase<LoginResponse>, IHasConnectionId
{
    public string Token { get; set; }
    public string? ConnectionId { get; set; }
}
