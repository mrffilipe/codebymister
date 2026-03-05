using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Maintenance.Commands.DeleteMaintenance;

public class DeleteMaintenance : IDeleteMaintenance
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMaintenance(IMaintenanceRepository maintenanceRepository, IUnitOfWork unitOfWork)
    {
        _maintenanceRepository = maintenanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var maintenance = await _maintenanceRepository.GetByIdAsync(id, cancellationToken);
        if (maintenance == null)
            return false;

        _maintenanceRepository.Delete(maintenance);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}



