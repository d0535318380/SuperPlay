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
    
    
    public readonly static User DefaultUser = User.Create("Default User");
    public readonly static User DefaultFriend = User.Create("Default Friend");
}