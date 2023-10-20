using MessagePack;

namespace SuperPlay.Abstractions.Mediator;

[MessagePackObject(keyAsPropertyName: true)]
public abstract class RequestBase<T> : IHasId, IRequest<T> where T : class
{
    public string Id { get; set; } = Guid.NewGuid().ToString("D");
}