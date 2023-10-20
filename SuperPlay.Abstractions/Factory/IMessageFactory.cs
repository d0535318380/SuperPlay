using SuperPlay.Abstractions.Domain;

namespace SuperPlay.Abstractions.Factory;

public interface IMessageFactory
{
    object Create(GenericMessage message);
}