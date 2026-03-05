using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface IProposalRepository
{
    Task<Proposal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Proposal>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default);
    Task<List<Proposal>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Proposal proposal, CancellationToken cancellationToken = default);
    void Update(Proposal proposal);
    void Delete(Proposal proposal);
}
