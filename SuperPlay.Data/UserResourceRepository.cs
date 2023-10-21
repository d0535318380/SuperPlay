using Microsoft.EntityFrameworkCore;
using SuperPlay.Abstractions.Data;

namespace SuperPlay.Data;

public class UserResourceRepository : RepositoryGeneric<int, UserResource>, IResourceRepository
{
    public UserResourceRepository(ApplicationDbContext context) : base(context)
    {
    }
}