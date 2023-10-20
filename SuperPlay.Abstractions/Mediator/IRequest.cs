namespace SuperPlay.Abstractions.Mediator;

public interface IBaseRequest
{
    
}

public interface IRequest<out T> : IBaseRequest
    where T: class
{
    
}