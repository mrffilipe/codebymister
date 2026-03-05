using Codebymister.Application.UseCases.Conversations.Commands.CreateConversation;
using Codebymister.Application.UseCases.Conversations.Commands.DeleteConversation;
using Codebymister.Application.UseCases.Conversations.Queries.GetAllConversations;
using Codebymister.Application.UseCases.Conversations.Queries.GetConversationById;
using Codebymister.Application.UseCases.Conversations.Commands.UpdateConversation;
using Codebymister.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

[Authorize]
public class ConversationsController : V1BaseController
{
    private readonly ICreateConversation _createConversation;
    private readonly IGetAllConversations _getAllConversations;
    private readonly IGetConversationById _getConversationById;
    private readonly IUpdateConversation _updateConversation;
    private readonly IDeleteConversation _deleteConversation;

    public ConversationsController(
        ICreateConversation createConversation,
        IGetAllConversations getAllConversations,
        IGetConversationById getConversationById,
        IUpdateConversation updateConversation,
        IDeleteConversation deleteConversation)
    {
        _createConversation = createConversation;
        _getAllConversations = getAllConversations;
        _getConversationById = getConversationById;
        _updateConversation = updateConversation;
        _deleteConversation = deleteConversation;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConversationRequest request, CancellationToken cancellationToken)
    {
        var conversation = await _createConversation.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = conversation.Id }, conversation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var conversations = await _getAllConversations.ExecuteAsync(cancellationToken);
        return Ok(conversations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var conversation = await _getConversationById.ExecuteAsync(id, cancellationToken);
        if (conversation == null)
            return NotFound();

        return Ok(conversation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateConversationRequest request, CancellationToken cancellationToken)
    {
        var conversation = await _updateConversation.ExecuteAsync(id, request, cancellationToken);
        if (conversation == null)
            return NotFound();

        return Ok(conversation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _deleteConversation.ExecuteAsync(id, cancellationToken);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
