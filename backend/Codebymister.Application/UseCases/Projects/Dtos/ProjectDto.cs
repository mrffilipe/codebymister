using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Projects.Dtos;

public record ProjectDto(
    Guid Id,
    Guid LeadId,
    string LeadName,
    ProjectType ProjectType,
    decimal ClosedValue,
    DateTime StartDate,
    DateTime? Deadline,
    ProjectStatus Status,
    bool EntryPaymentReceived,
    decimal? EntryPaymentValue,
    DateTime? EntryPaymentDate,
    bool FinalPaymentReceived,
    decimal? FinalPaymentValue,
    DateTime? FinalPaymentDate,
    string ScopeSummary,
    DateTime CreatedAt,
    DateTime? UpdatedAt
)
{
    public static ProjectDto FromEntity(Domain.Entities.Project project, string leadName)
    {
        return new ProjectDto(
            project.Id,
            project.LeadId,
            leadName,
            project.ProjectType,
            project.ClosedValue,
            project.StartDate,
            project.Deadline,
            project.Status,
            project.EntryPaymentReceived,
            project.EntryPaymentValue,
            project.EntryPaymentDate,
            project.FinalPaymentReceived,
            project.FinalPaymentValue,
            project.FinalPaymentDate,
            project.ScopeSummary,
            project.CreatedAt,
            project.UpdatedAt
        );
    }
};
