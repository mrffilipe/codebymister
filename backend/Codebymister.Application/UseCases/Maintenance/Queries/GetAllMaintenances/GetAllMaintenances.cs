using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Maintenance.Dtos;

namespace Codebymister.Application.UseCases.Maintenance.Queries.GetAllMaintenances;

public class GetAllMaintenances : IGetAllMaintenances
{
    private readonly IMaintenanceQueries _queries;

    public GetAllMaintenances(IMaintenanceQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<MaintenanceDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}


