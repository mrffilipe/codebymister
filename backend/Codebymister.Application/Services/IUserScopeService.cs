using Codebymister.Application.Common;

namespace Codebymister.Application.Services;

public interface IUserScopeService
{
    Guid UserId { get; }
    string ExternalAuthId { get; }
    string Email { get; }
    Guid? SessionId { get; }

    void Set(UserContext context, Guid sessionId);
}
