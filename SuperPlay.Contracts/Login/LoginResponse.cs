using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Login;

public class LoginResponse : BaseResponse
{
    public Guid UserId { get; set; }
}