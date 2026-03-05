using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class ConversationMapping : BaseEntityConfiguration<Conversation>
{
    public override void Configure(EntityTypeBuilder<Conversation> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("conversations");
        
        builder.Property(c => c.LeadId).HasColumnName("lead_id").IsRequired();
        builder.Property(c => c.InterestLevel).HasColumnName("interest_level").IsRequired();
        builder.Property(c => c.Timing).HasColumnName("timing").IsRequired();
        builder.Property(c => c.Notes).HasColumnName("notes").HasMaxLength(2000).IsRequired();
        builder.Property(c => c.NextStep).HasColumnName("next_step").HasMaxLength(500);
        builder.Property(c => c.Status).HasColumnName("status").IsRequired();
        builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        
        builder.HasOne(c => c.Lead)
            .WithMany()
            .HasForeignKey(c => c.LeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
