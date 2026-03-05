using Codebymister.Domain.Entities;

namespace Codebymister.Application.Services;

public interface IUserRepository
{
    Task<User?> GetByExternalAuthIdAsync(string externalAuthId, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    void Update(User user);
}
