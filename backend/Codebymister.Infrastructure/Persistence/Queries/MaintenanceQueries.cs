using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Maintenance.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Queries;

public class MaintenanceQueries : IMaintenanceQueries
{
    private readonly ApplicationDbContext _context;

    public MaintenanceQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var maintenance = await _context.Maintenances
            .AsNoTracking()
            .Include(m => m.Project)
                .ThenInclude(p => p.Lead)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (maintenance == null)
            return null;

        return MaintenanceDto.FromEntity(maintenance, maintenance.Project.Lead.Name);
    }

    public async Task<List<MaintenanceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var maintenances = await _context.Maintenances
            .AsNoTracking()
            .Include(m => m.Project)
                .ThenInclude(p => p.Lead)
            .OrderByDescending(m => m.StartDate)
            .ToListAsync(cancellationToken);

        return maintenances.Select(m => MaintenanceDto.FromEntity(m, m.Project.Lead.Name)).ToList();
    }
}
