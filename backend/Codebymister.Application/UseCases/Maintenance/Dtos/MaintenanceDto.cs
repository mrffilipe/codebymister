using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Maintenance.Dtos;

public record MaintenanceDto(
    Guid Id,
    Guid ProjectId,
    string ProjectName,
    decimal MonthlyValue,
    DateTime StartDate,
    MaintenanceStatus Status,
    DateTime NextBillingDate,
    bool HostingIncluded,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt
)
{
    public static MaintenanceDto FromEntity(Domain.Entities.Maintenance maintenance, string projectName)
    {
        return new MaintenanceDto(
            maintenance.Id,
            maintenance.ProjectId,
            projectName,
            maintenance.MonthlyValue,
            maintenance.StartDate,
            maintenance.Status,
            maintenance.NextBillingDate,
            maintenance.HostingIncluded,
            maintenance.Notes,
            maintenance.CreatedAt,
            maintenance.UpdatedAt
        );
    }
};
