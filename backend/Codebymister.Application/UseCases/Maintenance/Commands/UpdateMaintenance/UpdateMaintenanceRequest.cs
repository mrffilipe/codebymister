using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Maintenance.Commands.UpdateMaintenance;

public record UpdateMaintenanceRequest(
    bool UpdateBasicInfo = false,
    decimal? MonthlyValue = null,
    bool? HostingIncluded = null,
    string? Notes = null,
    bool UpdateStatus = false,
    MaintenanceStatus? Status = null,
    bool UpdateNextBillingDate = false,
    DateTime? NextBillingDate = null,
    bool ProcessBilling = false
);


