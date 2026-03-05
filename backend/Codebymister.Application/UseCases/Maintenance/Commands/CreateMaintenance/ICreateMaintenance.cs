using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Commands.CreateMaintenance;

public interface ICreateMaintenance
{
    Task<MaintenanceDto> ExecuteAsync(CreateMaintenanceRequest request, CancellationToken cancellationToken = default);
}
