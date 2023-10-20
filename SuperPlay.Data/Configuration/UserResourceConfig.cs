using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SuperPlay.Data.Configuration;

public class UserResourceConfig : IEntityTypeConfiguration<UserResource>
{
    public void Configure(EntityTypeBuilder<UserResource> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ResourceType)
            .IsRequired();

        builder.Property(x => x.Key)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();
        
        builder
            .HasIndex(x => new { x.UserId, x.ResourceType, ValueType = x.Key })
            .IsUnique();
    }
}