using Codebymister.Application.UseCases.Dashboard.Dtos;

namespace Codebymister.Application.UseCases.Dashboard.GetDashboardData;

public interface IGetDashboardData
{
    Task<DashboardDto> ExecuteAsync(CancellationToken cancellationToken = default);
}
