using SuperPlay.Abstractions.Domain;
using SuperPlay.Abstractions.Mediator;

namespace SuperPlay.Abstractions.Factory;

public interface IMessageFactory
{
    IBaseRequest FromRequest(GenericMessage message);
    IBaseResponse FromResponse(GenericMessage message);
}