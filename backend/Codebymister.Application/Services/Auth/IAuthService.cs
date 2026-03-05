using Codebymister.Application.Common;

namespace Codebymister.Application.Services.Auth;

public interface IAuthService
{
    Task<AuthSession> ExchangeTokenAsync(ExchangeTokenRequest request, CancellationToken cancellationToken);
    Task<AuthSession> RefreshAsync(CancellationToken cancellationToken);
}
