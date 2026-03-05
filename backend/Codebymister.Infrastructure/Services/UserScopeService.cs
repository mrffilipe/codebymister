using Codebymister.Application.Common;
using Codebymister.Application.Services;

namespace Codebymister.Infrastructure.Services;

public sealed class UserScopeService : IUserScopeService
{
    private bool _isSet;

    public Guid UserId { get; private set; }
    public string ExternalAuthId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Guid? SessionId { get; private set; }

    public void Set(UserContext context, Guid sessionId)
    {
        if (_isSet)
            throw new InvalidOperationException("UserScopeService já foi inicializado para este request.");

        _isSet = true;
        UserId = context.UserId;
        ExternalAuthId = context.ExternalAuthId;
        Email = context.Email;
        SessionId = sessionId;
    }
}
