using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Commands.UpdateConversation;

public interface IUpdateConversation
{
    Task<ConversationDto?> ExecuteAsync(Guid id, UpdateConversationRequest request, CancellationToken cancellationToken = default);
}
