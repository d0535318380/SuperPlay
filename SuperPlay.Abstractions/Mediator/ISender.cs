namespace SuperPlay.Abstractions.Mediator;

public interface ISender
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken token = default) 
        where TResponse : class;

    Task<IBaseResponse> SendAsync(IBaseRequest request, CancellationToken token = default);
}