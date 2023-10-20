using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Login;



public class LoginRequest : RequestBase<LoginResponse>
{
    public string Token { get; set; }
}