using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Events;
using SuperPlay.Contracts.Gift;
using SuperPlay.Data;

namespace SuperPlay.Handlers;

public class SendGiftHandler : RequestHandlerBase<SendGiftCommand, SendGiftResponse>
{
    private readonly IResourceRepository _repository;
    private readonly IMediator _publisher;

    public SendGiftHandler(
        IResourceRepository repository,
        IMediator publisher,
        ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        _repository = repository;
        _publisher = publisher;
    }

    protected override async Task<SendGiftResponse> HandleInternalAsync(SendGiftCommand request,
        CancellationToken cancellationToken)
    {
        var source =
            await _repository.DecreaseWalletAsync(
                request.UserId, request.Item.Key, request.Item.Value,
                cancellationToken);

        var destination = await _repository.IncreaseWalletAsync(
            request.ToUserId, request.Item.Key, request.Item.Value,
            cancellationToken);

        var payload = GiftEvent.Create(request.UserId, destination);
        var notification = UserNotificationEvent.Create(request.ToUserId, payload);
        
        await _publisher.PublishAsync(notification, cancellationToken);
        
        return SendGiftResponse.Create(source);
    }
}