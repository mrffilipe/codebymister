using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;

namespace Codebymister.Domain.Entities;

public class Proposal : BaseEntity
{
    public Guid LeadId { get; private set; }
    public ProjectType ProjectType { get; private set; }
    public decimal ProposedValue { get; private set; }
    public DateTime SentAt { get; private set; }
    public ProposalStatus Status { get; private set; }
    public string? RefusalReason { get; private set; }
    public DateTime? ResponseAt { get; private set; }
    public string? Notes { get; private set; }

    public Lead Lead { get; private set; }

    private Proposal() { }

    public Proposal(
        Guid leadId,
        ProjectType projectType,
        decimal proposedValue,
        string? notes = null)
    {
        LeadId = leadId;
        ProjectType = projectType;
        ProposedValue = proposedValue;
        SentAt = DateTime.UtcNow;
        Status = ProposalStatus.Sent;
        Notes = notes;
    }

    public void MarkAsUnderReview()
    {
        Status = ProposalStatus.UnderReview;
    }

    public void Accept()
    {
        Status = ProposalStatus.Accepted;
        ResponseAt = DateTime.UtcNow;
    }

    public void Refuse(string reason)
    {
        Status = ProposalStatus.Refused;
        RefusalReason = reason;
        ResponseAt = DateTime.UtcNow;
    }

    public void MarkAsExpired()
    {
        Status = ProposalStatus.Expired;
    }

    public void UpdateNotes(string notes)
    {
        Notes = notes;
    }
}
