using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Projects.Commands.DeleteProject;

public class DeleteProject : IDeleteProject
{
    private readonly IProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProject(IProjectRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _repository.GetByIdAsync(id, cancellationToken);
        if (project == null)
            return false;

        _repository.Delete(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



