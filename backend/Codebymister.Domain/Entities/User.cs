using Codebymister.Domain.Common;

namespace Codebymister.Domain.Entities;

public sealed class User : BaseEntity
{
    public string ExternalAuthId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? DisplayName { get; private set; }

    public ICollection<UserSession> Sessions { get; private set; } = new List<UserSession>();

    private User() { }

    public User(string externalAuthId, string email, string? displayName = null)
    {
        if (string.IsNullOrWhiteSpace(externalAuthId))
            throw new ArgumentException("ExternalAuthId is required.", nameof(externalAuthId));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        ExternalAuthId = externalAuthId;
        Email = email.Trim().ToLowerInvariant();
        DisplayName = displayName?.Trim();
    }

    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        Email = email.Trim().ToLowerInvariant();
    }

    public void UpdateDisplayName(string? displayName)
    {
        DisplayName = displayName?.Trim();
    }
}
