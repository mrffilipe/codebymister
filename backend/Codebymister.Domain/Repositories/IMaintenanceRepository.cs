using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface IMaintenanceRepository
{
    Task<Maintenance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Maintenance>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<List<Maintenance>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Maintenance maintenance, CancellationToken cancellationToken = default);
    void Update(Maintenance maintenance);
    void Delete(Maintenance maintenance);
}
