using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Commands.CreateProject;

public interface ICreateProject
{
    Task<ProjectDto> ExecuteAsync(CreateProjectRequest request, CancellationToken cancellationToken = default);
}
