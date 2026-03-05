using Codebymister.Application.UseCases.Dashboard.Dtos;
using Codebymister.Application.UseCases.Dashboard.GetDashboardData;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class DashboardController : V1BaseController
{
    private readonly IGetDashboardData _getDashboardData;

    public DashboardController(IGetDashboardData getDashboardData)
    {
        _getDashboardData = getDashboardData;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await _getDashboardData.ExecuteAsync(cancellationToken);
        return Ok(result);
    }
}
