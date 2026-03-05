using Codebymister.Application.UseCases.Maintenance.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Maintenance.Commands.UpdateMaintenance;

public class UpdateMaintenance : IUpdateMaintenance
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMaintenance(IMaintenanceRepository maintenanceRepository, IUnitOfWork unitOfWork)
    {
        _maintenanceRepository = maintenanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MaintenanceDto?> ExecuteAsync(Guid id, UpdateMaintenanceRequest request, CancellationToken cancellationToken = default)
    {
        var maintenance = await _maintenanceRepository.GetByIdAsync(id, cancellationToken);
        if (maintenance == null)
            return null;

        if (request.UpdateBasicInfo)
        {
            maintenance.Update(
                request.MonthlyValue!.Value,
                request.HostingIncluded!.Value,
                request.Notes
            );
        }

        if (request.UpdateStatus && request.Status.HasValue)
        {
            maintenance.UpdateStatus(request.Status.Value);
        }

        if (request.UpdateNextBillingDate && request.NextBillingDate.HasValue)
        {
            maintenance.UpdateNextBillingDate(request.NextBillingDate.Value);
        }

        if (request.ProcessBilling)
        {
            maintenance.ProcessBilling();
        }

        _maintenanceRepository.Update(maintenance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MaintenanceDto.FromEntity(maintenance, maintenance.Project.Lead.Name);
    }
}



