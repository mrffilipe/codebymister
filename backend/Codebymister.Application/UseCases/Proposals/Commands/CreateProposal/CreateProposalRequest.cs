using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Proposals.Commands.CreateProposal;

public record CreateProposalRequest(
    Guid LeadId,
    ProjectType ProjectType,
    decimal ProposedValue,
    string? Notes
);


