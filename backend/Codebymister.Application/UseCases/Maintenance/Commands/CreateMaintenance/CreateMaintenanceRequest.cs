namespace Codebymister.Application.UseCases.Maintenance.Commands.CreateMaintenance;

public record CreateMaintenanceRequest(
    Guid ProjectId,
    decimal MonthlyValue,
    DateTime StartDate,
    bool HostingIncluded,
    string? Notes
);


