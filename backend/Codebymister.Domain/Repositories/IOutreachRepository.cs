using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface IOutreachRepository
{
    Task<Outreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Outreach>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default);
    Task<List<Outreach>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Outreach outreach, CancellationToken cancellationToken = default);
    void Update(Outreach outreach);
    void Delete(Outreach outreach);
}
