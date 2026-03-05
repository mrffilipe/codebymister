using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;

namespace Codebymister.Domain.Entities;

public class Maintenance : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public decimal MonthlyValue { get; private set; }
    public DateTime StartDate { get; private set; }
    public MaintenanceStatus Status { get; private set; }
    public DateTime NextBillingDate { get; private set; }
    public bool HostingIncluded { get; private set; }
    public string? Notes { get; private set; }

    public Project Project { get; private set; }

    private Maintenance() { }

    public Maintenance(
        Guid projectId,
        decimal monthlyValue,
        DateTime startDate,
        bool hostingIncluded,
        string? notes = null)
    {
        ProjectId = projectId;
        MonthlyValue = monthlyValue;
        StartDate = startDate;
        Status = MaintenanceStatus.Active;
        NextBillingDate = startDate.AddMonths(1);
        HostingIncluded = hostingIncluded;
        Notes = notes;
    }

    public void UpdateStatus(MaintenanceStatus status)
    {
        Status = status;
    }

    public void UpdateNextBillingDate(DateTime date)
    {
        NextBillingDate = date;
    }

    public void Update(
        decimal monthlyValue,
        bool hostingIncluded,
        string? notes)
    {
        MonthlyValue = monthlyValue;
        HostingIncluded = hostingIncluded;
        Notes = notes;
    }

    public void ProcessBilling()
    {
        NextBillingDate = NextBillingDate.AddMonths(1);
    }
}
