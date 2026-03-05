using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Project project, CancellationToken cancellationToken = default);
    void Update(Project project);
    void Delete(Project project);
}
