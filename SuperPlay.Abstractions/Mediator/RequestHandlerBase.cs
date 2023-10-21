using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Extensions;

namespace SuperPlay.Abstractions.Mediator;

public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : class
{
    private readonly ILogger _logger;

    protected RequestHandlerBase(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        request.ThrowIfNull(nameof(request));
        
        try
        {
            var response = await HandleInternalAsync(request, cancellationToken);
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while handling request {RequestType}", typeof(TRequest));

            throw;
        }
    }

    protected  abstract Task<TResponse> HandleInternalAsync(TRequest request, CancellationToken cancellationToken);
}