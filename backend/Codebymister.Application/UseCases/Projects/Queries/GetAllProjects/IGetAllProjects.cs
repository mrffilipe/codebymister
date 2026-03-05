using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Queries.GetAllProjects;

public interface IGetAllProjects
{
    Task<List<ProjectDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}
