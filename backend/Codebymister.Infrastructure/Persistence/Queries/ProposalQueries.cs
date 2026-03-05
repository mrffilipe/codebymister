using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Proposals.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class ProposalQueries : IProposalQueries
{
    private readonly ApplicationDbContext _context;

    public ProposalQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProposalDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var proposal = await _context.Proposals
            .AsNoTracking()
            .Include(p => p.Lead)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (proposal == null)
            return null;

        return new ProposalDto(
            proposal.Id,
            proposal.LeadId,
            proposal.Lead.Name,
            proposal.ProjectType,
            proposal.ProposedValue,
            proposal.SentAt,
            proposal.Status,
            proposal.RefusalReason,
            proposal.ResponseAt,
            proposal.Notes
        );
    }

    public async Task<List<ProposalDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var proposals = await _context.Proposals
            .AsNoTracking()
            .Include(p => p.Lead)
            .ToListAsync(cancellationToken);

        return proposals.Select(p => new ProposalDto(
            p.Id,
            p.LeadId,
            p.Lead.Name,
            p.ProjectType,
            p.ProposedValue,
            p.SentAt,
            p.Status,
            p.RefusalReason,
            p.ResponseAt,
            p.Notes
        )).ToList();
    }
}
