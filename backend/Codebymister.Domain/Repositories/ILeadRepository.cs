using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface ILeadRepository
{
    Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Lead>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Lead lead, CancellationToken cancellationToken = default);
    void Update(Lead lead);
    void Delete(Lead lead);
}
