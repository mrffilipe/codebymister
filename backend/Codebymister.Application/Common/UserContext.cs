namespace Codebymister.Application.Common;

public sealed class UserContext
{
    public Guid UserId { get; init; }
    public string ExternalAuthId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
