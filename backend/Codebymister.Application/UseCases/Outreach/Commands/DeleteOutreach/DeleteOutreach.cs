using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Outreach.Commands.DeleteOutreach;

public class DeleteOutreach : IDeleteOutreach
{
    private readonly IOutreachRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOutreach(IOutreachRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var outreach = await _repository.GetByIdAsync(id, cancellationToken);
        if (outreach == null)
            return false;

        _repository.Delete(outreach);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



