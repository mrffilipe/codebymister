using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Lead)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Project>> GetByLeadIdAsync(Guid leadId, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Lead)
            .Where(p => p.LeadId == leadId)
            .OrderByDescending(p => p.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Lead)
            .OrderByDescending(p => p.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
    }

    public void Update(Project project)
    {
        _context.Projects.Update(project);
    }

    public void Delete(Project project)
    {
        _context.Projects.Remove(project);
    }
}
