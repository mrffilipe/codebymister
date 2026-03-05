using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Queries.GetProposalById;

public interface IGetProposalById
{
    Task<ProposalDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
