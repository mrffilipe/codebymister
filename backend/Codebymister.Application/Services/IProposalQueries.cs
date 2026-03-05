using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.Services;

public interface IProposalQueries
{
    Task<ProposalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ProposalDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
