﻿using Microsoft.Extensions.Logging;
using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Mediator;
using SuperPlay.Contracts.Login;
using SuperPlay.Contracts.Resources;
using SuperPlay.Data;

namespace SuperPlay.Handlers;

public class UpdateResourceHandler : RequestHandlerBase<UpdateResourcesCommand, UpdateResourcesResponse>
{
    private readonly IResourceRepository _resourceRepository;

    public UpdateResourceHandler(
        IResourceRepository resourceRepository,
        ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        _resourceRepository = resourceRepository;
    }

    protected override async Task<UpdateResourcesResponse> HandleInternalAsync(UpdateResourcesCommand request, CancellationToken cancellationToken)
    {
        var item = await _resourceRepository.UpdateWalletAsync(request.UserId, request.Item.Key, request.Item.Value, cancellationToken);

        return UpdateResourcesResponse.Create(item);
    }
}