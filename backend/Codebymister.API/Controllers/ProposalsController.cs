using Codebymister.Application.UseCases.Proposals.Commands.CreateProposal;
using Codebymister.Application.UseCases.Proposals.Commands.DeleteProposal;
using Codebymister.Application.UseCases.Proposals.Queries.GetAllProposals;
using Codebymister.Application.UseCases.Proposals.Queries.GetProposalById;
using Codebymister.Application.UseCases.Proposals.Commands.UpdateProposal;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class ProposalsController : V1BaseController
{
    private readonly ICreateProposal _createProposal;
    private readonly IGetAllProposals _getAllProposals;
    private readonly IGetProposalById _getProposalById;
    private readonly IUpdateProposal _updateProposal;
    private readonly IDeleteProposal _deleteProposal;

    public ProposalsController(
        ICreateProposal createProposal,
        IGetAllProposals getAllProposals,
        IGetProposalById getProposalById,
        IUpdateProposal updateProposal,
        IDeleteProposal deleteProposal)
    {
        _createProposal = createProposal;
        _getAllProposals = getAllProposals;
        _getProposalById = getProposalById;
        _updateProposal = updateProposal;
        _deleteProposal = deleteProposal;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await _createProposal.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = proposal.Id }, proposal);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var proposals = await _getAllProposals.ExecuteAsync(cancellationToken);
        return Ok(proposals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var proposal = await _getProposalById.ExecuteAsync(id, cancellationToken);
        if (proposal == null)
            return NotFound();

        return Ok(proposal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await _updateProposal.ExecuteAsync(id, request, cancellationToken);
        if (proposal == null)
            return NotFound();

        return Ok(proposal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _deleteProposal.ExecuteAsync(id, cancellationToken);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
