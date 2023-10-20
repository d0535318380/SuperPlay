namespace SuperPlay.Abstractions.Mediator;

public interface IRequest<out T> : IBaseRequest
    where T: class
{
    
}