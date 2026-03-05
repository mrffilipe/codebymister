using Codebymister.Application.UseCases.Leads.Commands.CreateLead;
using Codebymister.Application.UseCases.Leads.Commands.DeleteLead;
using Codebymister.Application.UseCases.Leads.Dtos;
using Codebymister.Application.UseCases.Leads.Queries.GetAllLeads;
using Codebymister.Application.UseCases.Leads.Queries.GetLeadById;
using Codebymister.Application.UseCases.Leads.Commands.UpdateLead;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class LeadsController : V1BaseController
{
    private readonly ICreateLead _createLead;
    private readonly IGetAllLeads _getAllLeads;
    private readonly IGetLeadById _getLeadById;
    private readonly IUpdateLead _updateLead;
    private readonly IDeleteLead _deleteLead;

    public LeadsController(
        ICreateLead createLead,
        IGetAllLeads getAllLeads,
        IGetLeadById getLeadById,
        IUpdateLead updateLead,
        IDeleteLead deleteLead)
    {
        _createLead = createLead;
        _getAllLeads = getAllLeads;
        _getLeadById = getLeadById;
        _updateLead = updateLead;
        _deleteLead = deleteLead;
    }

    [HttpPost]
    public async Task<ActionResult<LeadDto>> Create([FromBody] CreateLeadRequest request, CancellationToken cancellationToken)
    {
        var result = await _createLead.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<LeadDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _getAllLeads.ExecuteAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeadDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getLeadById.ExecuteAsync(id, cancellationToken);
        
        if (result == null)
            return NotFound(new { message = "Lead não encontrado" });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LeadDto>> Update(Guid id, [FromBody] UpdateLeadRequest request, CancellationToken cancellationToken)
    {
        var result = await _updateLead.ExecuteAsync(id, request, cancellationToken);
        
        if (result == null)
            return NotFound(new { message = "Lead não encontrado" });

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _deleteLead.ExecuteAsync(id, cancellationToken);
        
        if (!result)
            return NotFound(new { message = "Lead não encontrado" });

        return NoContent();
    }
}
