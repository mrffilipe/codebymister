using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Projects.Commands.CreateProject;

public record CreateProjectRequest(
    Guid LeadId,
    ProjectType ProjectType,
    decimal ClosedValue,
    DateTime StartDate,
    string ScopeSummary,
    DateTime? Deadline
);


