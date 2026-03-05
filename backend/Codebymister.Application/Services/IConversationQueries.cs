using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.Services;

public interface IConversationQueries
{
    Task<ConversationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ConversationDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
