using SuperPlay.Abstractions.Data;

namespace SuperPlay.Data;

public class UserRepository : RepositoryGeneric<Guid, User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}