namespace Codebymister.Application.Services.Auth;

public sealed record ExchangeTokenRequest
{
    public required string FirebaseIdToken { get; init; }
}
