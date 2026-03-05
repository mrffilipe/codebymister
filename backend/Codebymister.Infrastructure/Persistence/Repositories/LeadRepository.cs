using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly ApplicationDbContext _context;

    public LeadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Leads
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<Lead>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Leads
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Lead lead, CancellationToken cancellationToken = default)
    {
        await _context.Leads.AddAsync(lead, cancellationToken);
    }

    public void Update(Lead lead)
    {
        _context.Leads.Update(lead);
    }

    public void Delete(Lead lead)
    {
        _context.Leads.Remove(lead);
    }
}
