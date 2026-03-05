namespace Codebymister.Application.UseCases.Maintenance.Dtos;

public record CreateMaintenanceRequest(
    Guid ProjectId,
    decimal MonthlyValue,
    DateTime StartDate,
    bool HostingIncluded,
    string? Notes = null
);
