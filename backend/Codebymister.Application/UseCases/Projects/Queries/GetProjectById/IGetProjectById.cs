using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Queries.GetProjectById;

public interface IGetProjectById
{
    Task<ProjectDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
