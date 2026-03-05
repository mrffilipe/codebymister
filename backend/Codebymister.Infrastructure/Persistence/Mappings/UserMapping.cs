using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public sealed class UserMapping : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users");

        builder.Property(u => u.ExternalAuthId)
            .HasColumnName("external_auth_id")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(u => u.ExternalAuthId)
            .IsUnique();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.DisplayName)
            .HasColumnName("display_name")
            .HasMaxLength(255);

        builder.HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
