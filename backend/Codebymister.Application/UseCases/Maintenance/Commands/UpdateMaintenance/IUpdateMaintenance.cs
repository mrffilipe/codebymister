using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Commands.UpdateMaintenance;

public interface IUpdateMaintenance
{
    Task<MaintenanceDto?> ExecuteAsync(Guid id, UpdateMaintenanceRequest request, CancellationToken cancellationToken = default);
}
