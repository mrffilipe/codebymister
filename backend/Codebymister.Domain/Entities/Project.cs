using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;

namespace Codebymister.Domain.Entities;

public class Project : BaseEntity
{
    public Guid LeadId { get; private set; }
    public ProjectType ProjectType { get; private set; }
    public decimal ClosedValue { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? Deadline { get; private set; }
    public ProjectStatus Status { get; private set; }
    public bool EntryPaymentReceived { get; private set; }
    public decimal? EntryPaymentValue { get; private set; }
    public DateTime? EntryPaymentDate { get; private set; }
    public bool FinalPaymentReceived { get; private set; }
    public decimal? FinalPaymentValue { get; private set; }
    public DateTime? FinalPaymentDate { get; private set; }
    public string ScopeSummary { get; private set; }

    public Lead Lead { get; private set; }

    private Project() { }

    public Project(
        Guid leadId,
        ProjectType projectType,
        decimal closedValue,
        DateTime startDate,
        string scopeSummary,
        DateTime? deadline = null)
    {
        LeadId = leadId;
        ProjectType = projectType;
        ClosedValue = closedValue;
        StartDate = startDate;
        Deadline = deadline;
        Status = ProjectStatus.NotStarted;
        EntryPaymentReceived = false;
        FinalPaymentReceived = false;
        ScopeSummary = scopeSummary;
    }

    public void UpdateStatus(ProjectStatus status)
    {
        Status = status;
    }

    public void MarkEntryPaymentReceived(decimal value)
    {
        EntryPaymentReceived = true;
        EntryPaymentValue = value;
        EntryPaymentDate = DateTime.UtcNow;
    }

    public void MarkFinalPaymentReceived(decimal value)
    {
        FinalPaymentReceived = true;
        FinalPaymentValue = value;
        FinalPaymentDate = DateTime.UtcNow;
    }

    public void Update(
        ProjectType projectType,
        decimal closedValue,
        DateTime startDate,
        DateTime? deadline,
        string scopeSummary)
    {
        ProjectType = projectType;
        ClosedValue = closedValue;
        StartDate = startDate;
        Deadline = deadline;
        ScopeSummary = scopeSummary;
    }
}
