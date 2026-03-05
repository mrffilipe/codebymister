using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Queries.GetAllProposals;

public interface IGetAllProposals
{
    Task<List<ProposalDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}
