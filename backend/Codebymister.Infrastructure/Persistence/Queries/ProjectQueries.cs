using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Projects.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class ProjectQueries : IProjectQueries
{
    private readonly ApplicationDbContext _context;

    public ProjectQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Lead)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (project == null)
            return null;

        return ProjectDto.FromEntity(project, project.Lead.Name);
    }

    public async Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var projects = await _context.Projects
            .AsNoTracking()
            .Include(p => p.Lead)
            .OrderByDescending(p => p.StartDate)
            .ToListAsync(cancellationToken);

        return projects.Select(p => ProjectDto.FromEntity(p, p.Lead.Name)).ToList();
    }
}
