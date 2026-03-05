using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Commands.UpdateProposal;

public interface IUpdateProposal
{
    Task<ProposalDto?> ExecuteAsync(Guid id, UpdateProposalRequest request, CancellationToken cancellationToken = default);
}
