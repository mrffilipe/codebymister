using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Queries.GetConversationById;

public interface IGetConversationById
{
    Task<ConversationDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
