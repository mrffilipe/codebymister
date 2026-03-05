using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;

namespace Codebymister.Domain.Entities;

public class Outreach : BaseEntity
{
    public Guid LeadId { get; private set; }
    public OutreachChannel Channel { get; private set; }
    public string Message { get; private set; }
    public DateTime SentAt { get; private set; }
    public bool Responded { get; private set; }
    public DateTime? ResponseAt { get; private set; }
    public ResponseStatus ResponseStatus { get; private set; }
    public bool FollowUpSent { get; private set; }
    public string? Notes { get; private set; }

    public Lead Lead { get; private set; }

    private Outreach() { }

    public Outreach(
        Guid leadId,
        OutreachChannel channel,
        string message,
        string? notes = null)
    {
        LeadId = leadId;
        Channel = channel;
        Message = message;
        SentAt = DateTime.UtcNow;
        Responded = false;
        ResponseStatus = ResponseStatus.NoResponse;
        FollowUpSent = false;
        Notes = notes;
    }

    public void MarkAsResponded(DateTime? responseAt, ResponseStatus status)
    {
        Responded = true;
        ResponseAt = responseAt ?? DateTime.UtcNow;
        ResponseStatus = status;
    }

    public void MarkFollowUpSent()
    {
        FollowUpSent = true;
    }

    public void UpdateNotes(string notes)
    {
        Notes = notes;
    }
}
