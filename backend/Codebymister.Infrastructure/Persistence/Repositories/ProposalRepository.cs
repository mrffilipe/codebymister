using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class ProposalRepository : IProposalRepository
{
    private readonly ApplicationDbContext _context;

    public ProposalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Proposal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Proposals
            .Include(p => p.Lead)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Proposal>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default)
    {
        return await _context.Proposals
            .Include(p => p.Lead)
            .Where(p => p.LeadId == leadId)
            .OrderByDescending(p => p.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Proposal>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Proposals
            .Include(p => p.Lead)
            .OrderByDescending(p => p.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Proposal proposal, CancellationToken cancellationToken = default)
    {
        await _context.Proposals.AddAsync(proposal, cancellationToken);
    }

    public void Update(Proposal proposal)
    {
        _context.Proposals.Update(proposal);
    }

    public void Delete(Proposal proposal)
    {
        _context.Proposals.Remove(proposal);
    }
}
