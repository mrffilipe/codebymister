using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Projects.Dtos;

public record UpdateProjectRequest(
    ProjectType ProjectType,
    decimal ClosedValue,
    DateTime StartDate,
    DateTime? Deadline,
    string ScopeSummary
);
