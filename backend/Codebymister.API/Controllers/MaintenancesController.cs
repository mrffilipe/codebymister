using Codebymister.Application.UseCases.Maintenance.Commands.CreateMaintenance;
using Codebymister.Application.UseCases.Maintenance.Commands.DeleteMaintenance;
using Codebymister.Application.UseCases.Maintenance.Queries.GetAllMaintenances;
using Codebymister.Application.UseCases.Maintenance.Queries.GetMaintenanceById;
using Codebymister.Application.UseCases.Maintenance.Commands.UpdateMaintenance;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class MaintenancesController : V1BaseController
{
    private readonly ICreateMaintenance _createMaintenance;
    private readonly IGetAllMaintenances _getAllMaintenances;
    private readonly IGetMaintenanceById _getMaintenanceById;
    private readonly IUpdateMaintenance _updateMaintenance;
    private readonly IDeleteMaintenance _deleteMaintenance;

    public MaintenancesController(
        ICreateMaintenance createMaintenance,
        IGetAllMaintenances getAllMaintenances,
        IGetMaintenanceById getMaintenanceById,
        IUpdateMaintenance updateMaintenance,
        IDeleteMaintenance deleteMaintenance)
    {
        _createMaintenance = createMaintenance;
        _getAllMaintenances = getAllMaintenances;
        _getMaintenanceById = getMaintenanceById;
        _updateMaintenance = updateMaintenance;
        _deleteMaintenance = deleteMaintenance;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceRequest request, CancellationToken cancellationToken)
    {
        var maintenance = await _createMaintenance.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = maintenance.Id }, maintenance);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var maintenances = await _getAllMaintenances.ExecuteAsync(cancellationToken);
        return Ok(maintenances);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var maintenance = await _getMaintenanceById.ExecuteAsync(id, cancellationToken);
        if (maintenance == null)
            return NotFound();

        return Ok(maintenance);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMaintenanceRequest request, CancellationToken cancellationToken)
    {
        var maintenance = await _updateMaintenance.ExecuteAsync(id, request, cancellationToken);
        if (maintenance == null)
            return NotFound();

        return Ok(maintenance);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _deleteMaintenance.ExecuteAsync(id, cancellationToken);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
