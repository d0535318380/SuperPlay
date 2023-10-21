namespace SuperPlay.Abstractions.Mediator;

public interface ISender
{
    Task<IBaseResponse> SendAsync(IBaseRequest request, CancellationToken token = default);
}