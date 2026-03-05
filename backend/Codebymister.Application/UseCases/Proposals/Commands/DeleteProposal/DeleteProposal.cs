using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Proposals.Commands.DeleteProposal;

public class DeleteProposal : IDeleteProposal
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProposal(IProposalRepository proposalRepository, IUnitOfWork unitOfWork)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proposal = await _proposalRepository.GetByIdAsync(id, cancellationToken);
        if (proposal == null)
            return false;

        _proposalRepository.Delete(proposal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



