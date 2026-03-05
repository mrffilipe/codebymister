using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Commands.UpdateProject;

public interface IUpdateProject
{
    Task<ProjectDto?> ExecuteAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default);
}
