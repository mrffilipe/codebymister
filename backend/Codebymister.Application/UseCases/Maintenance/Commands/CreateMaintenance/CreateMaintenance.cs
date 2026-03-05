using Codebymister.Application.UseCases.Maintenance.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Maintenance.Commands.CreateMaintenance;

public class CreateMaintenance : ICreateMaintenance
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMaintenance(
        IProjectRepository projectRepository,
        IMaintenanceRepository maintenanceRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _maintenanceRepository = maintenanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MaintenanceDto> ExecuteAsync(CreateMaintenanceRequest request, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
        if (project == null)
            throw new InvalidOperationException("Project not found");

        var maintenance = new Domain.Entities.Maintenance(
            request.ProjectId,
            request.MonthlyValue,
            request.StartDate,
            request.HostingIncluded,
            request.Notes
        );

        await _maintenanceRepository.AddAsync(maintenance, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MaintenanceDto.FromEntity(maintenance, project.Lead.Name);
    }
}



