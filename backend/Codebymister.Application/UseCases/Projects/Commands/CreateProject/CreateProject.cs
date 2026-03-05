using Codebymister.Application.UseCases.Projects.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Projects.Commands.CreateProject;

public class CreateProject : ICreateProject
{
    private readonly ILeadRepository _leadRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProject(
        ILeadRepository leadRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _leadRepository = leadRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectDto> ExecuteAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException("Lead not found");

        var project = new Domain.Entities.Project(
            request.LeadId,
            request.ProjectType,
            request.ClosedValue,
            request.StartDate,
            request.ScopeSummary,
            request.Deadline
        );

        await _projectRepository.AddAsync(project, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ProjectDto.FromEntity(project, lead.Name);
    }
}



