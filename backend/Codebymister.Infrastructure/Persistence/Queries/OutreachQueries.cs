using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Outreach.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class OutreachQueries : IOutreachQueries
{
    private readonly ApplicationDbContext _context;

    public OutreachQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OutreachDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var outreach = await _context.Outreaches
            .AsNoTracking()
            .Include(o => o.Lead)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        if (outreach == null)
            return null;

        return new OutreachDto(
            outreach.Id,
            outreach.LeadId,
            outreach.Lead.Name,
            outreach.Channel,
            outreach.Message,
            outreach.SentAt,
            outreach.Responded,
            outreach.ResponseAt,
            outreach.ResponseStatus,
            outreach.FollowUpSent,
            outreach.Notes
        );
    }

    public async Task<List<OutreachDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var outreaches = await _context.Outreaches
            .AsNoTracking()
            .Include(o => o.Lead)
            .ToListAsync(cancellationToken);

        return outreaches.Select(o => new OutreachDto(
            o.Id,
            o.LeadId,
            o.Lead.Name,
            o.Channel,
            o.Message,
            o.SentAt,
            o.Responded,
            o.ResponseAt,
            o.ResponseStatus,
            o.FollowUpSent,
            o.Notes
        )).ToList();
    }
}
