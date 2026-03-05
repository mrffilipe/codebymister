using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Leads.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class LeadQueries : ILeadQueries
{
    private readonly ApplicationDbContext _context;

    public LeadQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LeadDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var lead = await _context.Leads
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        if (lead == null)
            return null;

        return LeadDto.FromEntity(lead);
    }

    public async Task<List<LeadDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var leads = await _context.Leads
            .AsNoTracking()
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(cancellationToken);

        return leads.Select(LeadDto.FromEntity).ToList();
    }
}
