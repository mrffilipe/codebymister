using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Queries.GetAllConversations;

public interface IGetAllConversations
{
    Task<List<ConversationDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}
