using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Queries.GetConversationById;

public class GetConversationById : IGetConversationById
{
    private readonly IConversationQueries _queries;

    public GetConversationById(IConversationQueries queries)
    {
        _queries = queries;
    }

    public async Task<ConversationDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}


