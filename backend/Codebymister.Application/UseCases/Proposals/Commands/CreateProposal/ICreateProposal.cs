using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Commands.CreateProposal;

public interface ICreateProposal
{
    Task<ProposalDto> ExecuteAsync(CreateProposalRequest request, CancellationToken cancellationToken = default);
}
