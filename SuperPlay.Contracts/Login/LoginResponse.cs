using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Contracts.Login;

public class LoginResponse : IBaseResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid UserId { get; set; }
    public string? Message { get; set; }
    public RequestStatus Status { get; set; }
}

public enum RequestStatus
{
    Success,
    Failed
}