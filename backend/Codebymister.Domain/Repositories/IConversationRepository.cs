using Codebymister.Domain.Entities;

namespace Codebymister.Domain.Repositories;

public interface IConversationRepository
{
    Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Conversation>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default);
    Task<List<Conversation>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default);
    void Update(Conversation conversation);
    void Delete(Conversation conversation);
}
