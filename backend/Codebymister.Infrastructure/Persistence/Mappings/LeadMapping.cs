using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class LeadMapping : BaseEntityConfiguration<Lead>
{
    public override void Configure(EntityTypeBuilder<Lead> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("leads");
            
        builder.Property(l => l.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();
            
        builder.Property(l => l.Segment)
            .HasColumnName("segment")
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(l => l.City)
            .HasColumnName("city")
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(l => l.Website)
            .HasColumnName("website")
            .HasMaxLength(500)
            .HasConversion(
                v => v != null ? v.Value : null,
                v => v != null ? Codebymister.Domain.ValueObjects.Url.Create(v) : null);

        builder.Property(l => l.Instagram)
            .HasColumnName("instagram")
            .HasMaxLength(100)
            .HasConversion(
                v => v != null ? v.Value : null,
                v => v != null ? Codebymister.Domain.ValueObjects.Instagram.Create(v) : null);

        builder.Property(l => l.Phone)
            .HasColumnName("phone")
            .HasMaxLength(11)
            .HasConversion(
                v => v != null ? v.Value : null,
                v => v != null ? Codebymister.Domain.ValueObjects.Phone.Create(v) : null);
            
        builder.Property(l => l.ProblemDescription)
            .HasColumnName("problem_description")
            .HasMaxLength(1000)
            .IsRequired();
            
        builder.Property(l => l.Priority)
            .HasColumnName("priority")
            .IsRequired();
            
        builder.Property(l => l.Source)
            .HasColumnName("source")
            .IsRequired();
            
        builder.Property(l => l.AlreadyApproached)
            .HasColumnName("already_approached")
            .HasDefaultValue(false);
    }
}
