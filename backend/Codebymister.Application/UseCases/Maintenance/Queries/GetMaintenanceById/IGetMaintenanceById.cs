using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Queries.GetMaintenanceById;

public interface IGetMaintenanceById
{
    Task<MaintenanceDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
