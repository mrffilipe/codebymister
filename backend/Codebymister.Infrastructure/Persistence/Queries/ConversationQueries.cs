using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Conversations.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class ConversationQueries : IConversationQueries
{
    private readonly ApplicationDbContext _context;

    public ConversationQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ConversationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var conversation = await _context.Conversations
            .AsNoTracking()
            .Include(c => c.Lead)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (conversation == null)
            return null;

        return new ConversationDto(
            conversation.Id,
            conversation.LeadId,
            conversation.Lead.Name,
            conversation.InterestLevel,
            conversation.Timing,
            conversation.Notes,
            conversation.NextStep,
            conversation.Status,
            conversation.CreatedAt,
            conversation.UpdatedAt
        );
    }

    public async Task<List<ConversationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var conversations = await _context.Conversations
            .AsNoTracking()
            .Include(c => c.Lead)
            .ToListAsync(cancellationToken);

        return conversations.Select(c => new ConversationDto(
            c.Id,
            c.LeadId,
            c.Lead.Name,
            c.InterestLevel,
            c.Timing,
            c.Notes,
            c.NextStep,
            c.Status,
            c.CreatedAt,
            c.UpdatedAt
        )).ToList();
    }
}
