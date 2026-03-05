using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Codebymister.Infrastructure.Persistence.Mappings;

public class OutreachMapping : BaseEntityConfiguration<Outreach>
{
    public override void Configure(EntityTypeBuilder<Outreach> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("outreaches");
        
        builder.Property(o => o.LeadId).HasColumnName("lead_id").IsRequired();
        builder.Property(o => o.Channel).HasColumnName("channel").IsRequired();
        builder.Property(o => o.Message).HasColumnName("message").HasMaxLength(2000).IsRequired();
        builder.Property(o => o.SentAt).HasColumnName("sent_at").IsRequired();
        builder.Property(o => o.Responded).HasColumnName("responded").IsRequired();
        builder.Property(o => o.ResponseAt).HasColumnName("response_at");
        builder.Property(o => o.ResponseStatus).HasColumnName("response_status").IsRequired();
        builder.Property(o => o.FollowUpSent).HasColumnName("follow_up_sent").IsRequired();
        builder.Property(o => o.Notes).HasColumnName("notes").HasMaxLength(1000);
        
        builder.HasOne(o => o.Lead)
            .WithMany()
            .HasForeignKey(o => o.LeadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
