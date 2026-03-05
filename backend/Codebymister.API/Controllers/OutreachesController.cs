using Codebymister.Application.UseCases.Outreach.Commands.CreateOutreach;
using Codebymister.Application.UseCases.Outreach.Commands.DeleteOutreach;
using Codebymister.Application.UseCases.Outreach.Queries.GetAllOutreaches;
using Codebymister.Application.UseCases.Outreach.Queries.GetOutreachById;
using Codebymister.Application.UseCases.Outreach.Commands.UpdateOutreach;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class OutreachesController : V1BaseController
{
    private readonly ICreateOutreach _createOutreach;
    private readonly IGetAllOutreaches _getAllOutreaches;
    private readonly IGetOutreachById _getOutreachById;
    private readonly IUpdateOutreach _updateOutreach;
    private readonly IDeleteOutreach _deleteOutreach;

    public OutreachesController(
        ICreateOutreach createOutreach,
        IGetAllOutreaches getAllOutreaches,
        IGetOutreachById getOutreachById,
        IUpdateOutreach updateOutreach,
        IDeleteOutreach deleteOutreach)
    {
        _createOutreach = createOutreach;
        _getAllOutreaches = getAllOutreaches;
        _getOutreachById = getOutreachById;
        _updateOutreach = updateOutreach;
        _deleteOutreach = deleteOutreach;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOutreachRequest request, CancellationToken cancellationToken)
    {
        var outreach = await _createOutreach.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = outreach.Id }, outreach);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var outreaches = await _getAllOutreaches.ExecuteAsync(cancellationToken);
        return Ok(outreaches);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var outreach = await _getOutreachById.ExecuteAsync(id, cancellationToken);
        if (outreach == null)
            return NotFound();

        return Ok(outreach);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOutreachRequest request, CancellationToken cancellationToken)
    {
        var outreach = await _updateOutreach.ExecuteAsync(id, request, cancellationToken);
        if (outreach == null)
            return NotFound();

        return Ok(outreach);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _deleteOutreach.ExecuteAsync(id, cancellationToken);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
