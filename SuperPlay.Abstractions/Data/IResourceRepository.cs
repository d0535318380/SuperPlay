using SuperPlay.Contracts.Login;

namespace SuperPlay.Abstractions.Data;

public interface IResourceRepository : IGenericRepository<Guid, UserResource>
{
}