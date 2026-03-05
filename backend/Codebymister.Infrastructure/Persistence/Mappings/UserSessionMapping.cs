using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public sealed class UserSessionMapping : BaseEntityConfiguration<UserSession>
{
    public override void Configure(EntityTypeBuilder<UserSession> builder)
    {
        base.Configure(builder);

        builder.ToTable("user_sessions");

        builder.Property(s => s.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(s => s.IsRevoked)
            .HasColumnName("is_revoked")
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
