using Codebymister.Application.UseCases.Proposals.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Proposals.Commands.UpdateProposal;

public class UpdateProposal : IUpdateProposal
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProposal(IProposalRepository proposalRepository, IUnitOfWork unitOfWork)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProposalDto?> ExecuteAsync(Guid id, UpdateProposalRequest request, CancellationToken cancellationToken = default)
    {
        var proposal = await _proposalRepository.GetByIdAsync(id, cancellationToken);
        if (proposal == null)
            return null;

        switch (request.Action)
        {
            case ProposalAction.MarkAsUnderReview:
                proposal.MarkAsUnderReview();
                break;
            case ProposalAction.Accept:
                proposal.Accept();
                break;
            case ProposalAction.Refuse:
                if (string.IsNullOrWhiteSpace(request.RefusalReason))
                    throw new InvalidOperationException("Refusal reason is required");
                proposal.Refuse(request.RefusalReason);
                break;
            case ProposalAction.MarkAsExpired:
                proposal.MarkAsExpired();
                break;
            case ProposalAction.UpdateNotes:
                if (!string.IsNullOrWhiteSpace(request.Notes))
                    proposal.UpdateNotes(request.Notes);
                break;
        }

        _proposalRepository.Update(proposal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProposalDto(
            proposal.Id,
            proposal.LeadId,
            proposal.Lead.Name,
            proposal.ProjectType,
            proposal.ProposedValue,
            proposal.SentAt,
            proposal.Status,
            proposal.RefusalReason,
            proposal.ResponseAt,
            proposal.Notes
        );
    }
}



