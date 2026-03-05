using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class OutreachRepository : IOutreachRepository
{
    private readonly ApplicationDbContext _context;

    public OutreachRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Outreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Outreaches
            .Include(o => o.Lead)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<List<Outreach>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default)
    {
        return await _context.Outreaches
            .Include(o => o.Lead)
            .Where(o => o.LeadId == leadId)
            .OrderByDescending(o => o.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Outreach>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Outreaches
            .Include(o => o.Lead)
            .OrderByDescending(o => o.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Outreach outreach, CancellationToken cancellationToken = default)
    {
        await _context.Outreaches.AddAsync(outreach, cancellationToken);
    }

    public void Update(Outreach outreach)
    {
        _context.Outreaches.Update(outreach);
    }

    public void Delete(Outreach outreach)
    {
        _context.Outreaches.Remove(outreach);
    }
}
