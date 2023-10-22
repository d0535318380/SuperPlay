using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SuperPlay.Data.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder
            .Property(u => u.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Title);
    }
    
    
    public static readonly User DefaultUser = User.Create("Default User", id: Guid.Parse("94D5C79E-8ABA-4C82-A848-E67C42E74FB4"));
    public static readonly User DefaultFriend = User.Create("Default Friend", id: Guid.Parse("7BDCFD49-B815-4FEC-9993-C25DE37D6E20"));
}