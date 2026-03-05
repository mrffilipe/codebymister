using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.Services;

public interface IMaintenanceQueries
{
    Task<MaintenanceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<MaintenanceDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
