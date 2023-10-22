using Microsoft.EntityFrameworkCore;
using SuperPlay.Data.Configuration;

namespace SuperPlay.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task MigrateAsync()
    {
        
        await Database.MigrateAsync();

        var isDbEmpty = !Set<User>().Any();

        if (isDbEmpty == false)
        {
            return;
        }
        
        Set<User>()
            .AddRange(UserConfig.DefaultUser, UserConfig.DefaultFriend);
        
        await SaveChangesAsync();
    }
}


