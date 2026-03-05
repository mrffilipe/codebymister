using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;

namespace Codebymister.Domain.Entities;

public class Conversation : BaseEntity
{
    public Guid LeadId { get; private set; }
    public InterestLevel InterestLevel { get; private set; }
    public Timing Timing { get; private set; }
    public string Notes { get; private set; }
    public string? NextStep { get; private set; }
    public ConversationStatus Status { get; private set; }

    public Lead Lead { get; private set; }

    private Conversation() { }

    public Conversation(
        Guid leadId,
        InterestLevel interestLevel,
        Timing timing,
        string notes,
        string? nextStep = null)
    {
        LeadId = leadId;
        InterestLevel = interestLevel;
        Timing = timing;
        Notes = notes;
        NextStep = nextStep;
        Status = ConversationStatus.Active;
    }

    public void Update(
        InterestLevel interestLevel,
        Timing timing,
        string notes,
        string? nextStep,
        ConversationStatus status)
    {
        InterestLevel = interestLevel;
        Timing = timing;
        Notes = notes;
        NextStep = nextStep;
        Status = status;
    }

    public void UpdateStatus(ConversationStatus status)
    {
        Status = status;
    }
}
