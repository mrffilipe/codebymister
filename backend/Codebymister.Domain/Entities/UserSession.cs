using Codebymister.Domain.Common;

namespace Codebymister.Domain.Entities;

public sealed class UserSession : BaseEntity
{
    public Guid UserId { get; private set; }
    public bool IsRevoked { get; private set; }

    public User User { get; private set; } = default!;

    private UserSession() { }

    public UserSession(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required.", nameof(userId));

        UserId = userId;
        IsRevoked = false;
    }

    public void Revoke()
    {
        IsRevoked = true;
    }
}
