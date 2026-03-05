using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.Services;

public interface IProjectQueries
{
    Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
