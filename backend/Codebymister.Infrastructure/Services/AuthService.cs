using Codebymister.Application.Common;
using Codebymister.Application.Services;
using Codebymister.Application.Services.Auth;
using Codebymister.Domain.Entities;
using Codebymister.Domain.Services;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;

namespace Codebymister.Infrastructure.Services;

public sealed class AuthService : IAuthService
{
    private readonly FirebaseAuth _firebaseAuth;
    private readonly IUserScopeService _userScope;
    private readonly IUserRepository _userRepository;
    private readonly IUserSessionRepository _sessionRepository;
    private readonly IAppTokenIssuer _tokenIssuer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly int _accessTokenMinutes;

    public AuthService(
        FirebaseAuth firebaseAuth,
        IUserScopeService userScope,
        IUserRepository userRepository,
        IUserSessionRepository sessionRepository,
        IAppTokenIssuer tokenIssuer,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _firebaseAuth = firebaseAuth;
        _userScope = userScope;
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _tokenIssuer = tokenIssuer;
        _unitOfWork = unitOfWork;
        _accessTokenMinutes = int.TryParse(configuration["AppJwt:AccessTokenMinutes"], out var minutes)
            ? minutes
            : 60;
    }

    public async Task<AuthSession> ExchangeTokenAsync(ExchangeTokenRequest request, CancellationToken cancellationToken)
    {
        FirebaseToken decodedToken;
        try
        {
            decodedToken = await _firebaseAuth.VerifyIdTokenAsync(request.FirebaseIdToken, cancellationToken);
        }
        catch (Exception)
        {
            throw new Exception("Firebase token inválido ou expirado.");
        }

        var externalAuthId = decodedToken.Uid;
        var email = decodedToken.Claims.TryGetValue("email", out var emailClaim)
            ? emailClaim?.ToString() ?? string.Empty
            : string.Empty;
        var displayName = decodedToken.Claims.TryGetValue("name", out var nameClaim)
            ? nameClaim?.ToString()
            : null;

        var user = await _userRepository.GetByExternalAuthIdAsync(externalAuthId, cancellationToken);

        if (user is null)
        {
            user = new User(externalAuthId, email, displayName);
            await _userRepository.AddAsync(user, cancellationToken);
        }
        else
        {
            user.UpdateEmail(email);
            user.UpdateDisplayName(displayName);
            _userRepository.Update(user);
        }

        var session = new UserSession(user.Id);
        await _sessionRepository.AddAsync(session, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_accessTokenMinutes);
        var accessToken = _tokenIssuer.Issue(new AppTokenClaims(
            UserId: user.Id,
            ExternalAuthId: externalAuthId,
            SessionId: session.Id,
            Email: email));

        return new AuthSession
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            UserId = user.Id,
            ExternalAuthId = externalAuthId,
            Email = email
        };
    }

    public async Task<AuthSession> RefreshAsync(CancellationToken cancellationToken)
    {
        if (_userScope.SessionId is null)
            throw new Exception("Sessão inválida.");

        var session = await _sessionRepository.GetByIdAsync(_userScope.SessionId.Value, cancellationToken);
        if (session is null || session.IsRevoked || session.UserId != _userScope.UserId)
            throw new Exception("Sessão inválida ou revogada.");

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_accessTokenMinutes);
        var accessToken = _tokenIssuer.Issue(new AppTokenClaims(
            UserId: _userScope.UserId,
            ExternalAuthId: _userScope.ExternalAuthId,
            SessionId: session.Id,
            Email: _userScope.Email));

        return new AuthSession
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            UserId = _userScope.UserId,
            ExternalAuthId = _userScope.ExternalAuthId,
            Email = _userScope.Email
        };
    }
}
