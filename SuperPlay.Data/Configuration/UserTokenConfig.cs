using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SuperPlay.Data.Configuration;

public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(100);
        
        builder
            .HasIndex(x => new { x.UserId, x.Token })
            .IsUnique();
    }
}