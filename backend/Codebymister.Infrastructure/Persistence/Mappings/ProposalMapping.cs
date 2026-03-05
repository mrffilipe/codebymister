using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class ProposalMapping : BaseEntityConfiguration<Proposal>
{
    public override void Configure(EntityTypeBuilder<Proposal> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("proposals");
        
        builder.Property(p => p.LeadId).HasColumnName("lead_id").IsRequired();
        builder.Property(p => p.ProjectType).HasColumnName("project_type").IsRequired();
        builder.Property(p => p.ProposedValue).HasColumnName("proposed_value").HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.SentAt).HasColumnName("sent_at").IsRequired();
        builder.Property(p => p.Status).HasColumnName("status").IsRequired();
        builder.Property(p => p.RefusalReason).HasColumnName("refusal_reason").HasMaxLength(500);
        builder.Property(p => p.ResponseAt).HasColumnName("response_at");
        builder.Property(p => p.Notes).HasColumnName("notes").HasMaxLength(1000);
        
        builder.HasOne(p => p.Lead)
            .WithMany()
            .HasForeignKey(p => p.LeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
