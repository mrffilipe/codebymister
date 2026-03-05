using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Maintenance?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Maintenances
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<List<Maintenance>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Maintenances
            .Include(m => m.Project)
            .Where(m => m.ProjectId == projectId)
            .OrderByDescending(m => m.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Maintenance>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Maintenances
            .Include(m => m.Project)
            .OrderByDescending(m => m.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Maintenance maintenance, CancellationToken cancellationToken = default)
    {
        await _context.Maintenances.AddAsync(maintenance, cancellationToken);
    }

    public void Update(Maintenance maintenance)
    {
        _context.Maintenances.Update(maintenance);
    }

    public void Delete(Maintenance maintenance)
    {
        _context.Maintenances.Remove(maintenance);
    }
}
