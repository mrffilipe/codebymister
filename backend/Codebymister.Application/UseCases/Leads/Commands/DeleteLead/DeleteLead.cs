using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Leads.Commands.DeleteLead;

public class DeleteLead : IDeleteLead
{
    private readonly ILeadRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLead(ILeadRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var lead = await _repository.GetByIdAsync(id, cancellationToken);

        if (lead == null)
            return false;

        _repository.Delete(lead);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}


