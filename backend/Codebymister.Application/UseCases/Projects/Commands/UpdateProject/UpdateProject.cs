using Codebymister.Application.UseCases.Projects.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Projects.Commands.UpdateProject;

public class UpdateProject : IUpdateProject
{
    private readonly IProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProject(IProjectRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectDto?> ExecuteAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = await _repository.GetByIdAsync(id, cancellationToken);
        if (project == null)
            return null;

        if (request.UpdateBasicInfo)
        {
            project.Update(
                request.ProjectType!.Value,
                request.ClosedValue!.Value,
                request.StartDate!.Value,
                request.Deadline,
                request.ScopeSummary!
            );
        }

        if (request.UpdateStatus && request.Status.HasValue)
        {
            project.UpdateStatus(request.Status.Value);
        }

        if (request.MarkEntryPayment && request.EntryPaymentValue.HasValue)
        {
            project.MarkEntryPaymentReceived(request.EntryPaymentValue.Value);
        }

        if (request.MarkFinalPayment && request.FinalPaymentValue.HasValue)
        {
            project.MarkFinalPaymentReceived(request.FinalPaymentValue.Value);
        }

        _repository.Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ProjectDto.FromEntity(project, project.Lead.Name);
    }
}



