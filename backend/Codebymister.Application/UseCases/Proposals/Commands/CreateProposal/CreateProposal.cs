using Codebymister.Application.UseCases.Proposals.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Proposals.Commands.CreateProposal;

public class CreateProposal : ICreateProposal
{
    private readonly ILeadRepository _leadRepository;
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProposal(
        ILeadRepository leadRepository,
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork)
    {
        _leadRepository = leadRepository;
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProposalDto> ExecuteAsync(CreateProposalRequest request, CancellationToken cancellationToken = default)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException("Lead not found");

        var proposal = new Domain.Entities.Proposal(
            request.LeadId,
            request.ProjectType,
            request.ProposedValue,
            request.Notes
        );

        await _proposalRepository.AddAsync(proposal, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProposalDto(
            proposal.Id,
            proposal.LeadId,
            lead.Name,
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



