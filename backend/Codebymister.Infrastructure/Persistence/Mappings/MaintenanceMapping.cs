using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class MaintenanceMapping : BaseEntityConfiguration<Maintenance>
{
    public override void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("maintenances");
        
        builder.Property(m => m.ProjectId).HasColumnName("project_id").IsRequired();
        builder.Property(m => m.MonthlyValue).HasColumnName("monthly_value").HasPrecision(18, 2).IsRequired();
        builder.Property(m => m.StartDate).HasColumnName("start_date").IsRequired();
        builder.Property(m => m.Status).HasColumnName("status").IsRequired();
        builder.Property(m => m.NextBillingDate).HasColumnName("next_billing_date").IsRequired();
        builder.Property(m => m.HostingIncluded).HasColumnName("hosting_included").IsRequired();
        builder.Property(m => m.Notes).HasColumnName("notes").HasMaxLength(1000);
        builder.Property(m => m.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(m => m.UpdatedAt).HasColumnName("updated_at");
        
        builder.HasOne(m => m.Project)
            .WithMany()
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
