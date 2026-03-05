namespace Codebymister.Application.UseCases.Proposals.Commands.UpdateProposal;

public record UpdateProposalRequest(
    ProposalAction Action,
    string? RefusalReason = null,
    string? Notes = null
);

public enum ProposalAction
{
    MarkAsUnderReview,
    Accept,
    Refuse,
    MarkAsExpired,
    UpdateNotes
}


