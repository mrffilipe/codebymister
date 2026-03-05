using Codebymister.Application.Services;
using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codebymister.Infrastructure.Persistence.Repositories;

public sealed class UserSessionRepository : IUserSessionRepository
{
    private readonly ApplicationDbContext _context;

    public UserSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserSession?> GetByIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.UserSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
    }

    public async Task AddAsync(UserSession session, CancellationToken cancellationToken = default)
    {
        await _context.UserSessions.AddAsync(session, cancellationToken);
    }

    public void Update(UserSession session)
    {
        _context.UserSessions.Update(session);
    }
}
