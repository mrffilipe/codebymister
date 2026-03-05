namespace Codebymister.Application.UseCases.Maintenance.Dtos;

public record UpdateMaintenanceRequest(
    decimal MonthlyValue,
    bool HostingIncluded,
    string? Notes
);
