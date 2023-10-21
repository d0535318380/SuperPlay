using MessagePack;

namespace SuperPlay.Abstractions.Mediator;

public interface IBaseResponse : IHasStringId
{
    
}

[MessagePackObject(keyAsPropertyName: true)]
public abstract class  BaseResponse : IBaseResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString("P");
    public string? Message { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Success;
}