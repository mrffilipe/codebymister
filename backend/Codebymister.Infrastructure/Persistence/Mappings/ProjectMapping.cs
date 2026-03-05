using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class ProjectMapping : BaseEntityConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("projects");
        
        builder.Property(p => p.LeadId).HasColumnName("lead_id").IsRequired();
        builder.Property(p => p.ProjectType).HasColumnName("project_type").IsRequired();
        builder.Property(p => p.ClosedValue).HasColumnName("closed_value").HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.StartDate).HasColumnName("start_date").IsRequired();
        builder.Property(p => p.Deadline).HasColumnName("deadline");
        builder.Property(p => p.Status).HasColumnName("status").IsRequired();
        builder.Property(p => p.EntryPaymentReceived).HasColumnName("entry_payment_received").IsRequired();
        builder.Property(p => p.EntryPaymentValue).HasColumnName("entry_payment_value").HasPrecision(18, 2);
        builder.Property(p => p.EntryPaymentDate).HasColumnName("entry_payment_date");
        builder.Property(p => p.FinalPaymentReceived).HasColumnName("final_payment_received").IsRequired();
        builder.Property(p => p.FinalPaymentValue).HasColumnName("final_payment_value").HasPrecision(18, 2);
        builder.Property(p => p.FinalPaymentDate).HasColumnName("final_payment_date");
        builder.Property(p => p.ScopeSummary).HasColumnName("scope_summary").HasMaxLength(2000).IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
        
        builder.HasOne(p => p.Lead)
            .WithMany()
            .HasForeignKey(p => p.LeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
