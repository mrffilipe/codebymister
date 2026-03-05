using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Projects.Dtos;

public record CreateProjectRequest(
    Guid LeadId,
    ProjectType ProjectType,
    decimal ClosedValue,
    DateTime StartDate,
    string ScopeSummary,
    DateTime? Deadline = null
);
