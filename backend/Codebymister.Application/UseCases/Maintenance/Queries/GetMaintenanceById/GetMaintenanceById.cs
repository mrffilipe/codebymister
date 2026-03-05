using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Queries.GetMaintenanceById;

public class GetMaintenanceById : IGetMaintenanceById
{
    private readonly IMaintenanceQueries _queries;

    public GetMaintenanceById(IMaintenanceQueries queries)
    {
        _queries = queries;
    }

    public async Task<MaintenanceDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}


