using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Queries.GetAllMaintenances;

public interface IGetAllMaintenances
{
    Task<List<MaintenanceDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}
