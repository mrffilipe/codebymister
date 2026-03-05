namespace Codebymister.Application.Common;

public sealed record AuthSession
{
    public required string AccessToken { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required Guid UserId { get; init; }
    public required string ExternalAuthId { get; init; }
    public required string Email { get; init; }
}
