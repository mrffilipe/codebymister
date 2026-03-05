using Codebymister.Domain.Entities;

namespace Codebymister.Application.Services;

public interface IUserSessionRepository
{
    Task<UserSession?> GetByIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task AddAsync(UserSession session, CancellationToken cancellationToken = default);
    void Update(UserSession session);
}
