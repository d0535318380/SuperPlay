namespace SuperPlay.Abstractions.Mediator;

public interface IBaseResponse : IHasStringId
{
    
}

public abstract class  BaseResponse : IBaseResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString("P");
}