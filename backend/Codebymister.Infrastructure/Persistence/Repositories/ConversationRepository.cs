using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Conversations
            .Include(c => c.Lead)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<Conversation>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default)
    {
        return await _context.Conversations
            .Include(c => c.Lead)
            .Where(c => c.LeadId == leadId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Conversation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Conversations
            .Include(c => c.Lead)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        await _context.Conversations.AddAsync(conversation, cancellationToken);
    }

    public void Update(Conversation conversation)
    {
        _context.Conversations.Update(conversation);
    }

    public void Delete(Conversation conversation)
    {
        _context.Conversations.Remove(conversation);
    }
}
