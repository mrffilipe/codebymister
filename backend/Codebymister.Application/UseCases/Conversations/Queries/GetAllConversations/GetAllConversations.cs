using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Conversations.Dtos;

namespace Codebymister.Application.UseCases.Conversations.Queries.GetAllConversations;

public class GetAllConversations : IGetAllConversations
{
    private readonly IConversationQueries _queries;

    public GetAllConversations(IConversationQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<ConversationDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}


