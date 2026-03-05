using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Commands.CreateConversation;

public interface ICreateConversation
{
    Task<ConversationDto> ExecuteAsync(CreateConversationRequest request, CancellationToken cancellationToken = default);
}
