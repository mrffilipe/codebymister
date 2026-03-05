namespace Codebymister.Application.Common;

public sealed record AppTokenClaims(
    Guid UserId,
    string ExternalAuthId,
    Guid SessionId,
    string? Email);
