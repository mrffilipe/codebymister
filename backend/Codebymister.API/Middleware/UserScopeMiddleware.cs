using Codebymister.Application.Common;
using Codebymister.Application.Services;

namespace Codebymister.API.Middleware;

public sealed class UserScopeMiddleware : IMiddleware
{
    private readonly IUserScopeService _userScopeService;

    public UserScopeMiddleware(IUserScopeService userScopeService)
    {
        _userScopeService = userScopeService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            await next(context);
            return;
        }

        var uid = context.User.FindFirst("uid")?.Value;
        var sub = context.User.FindFirst("sub")?.Value;
        var sid = context.User.FindFirst("sid")?.Value;

        if (string.IsNullOrWhiteSpace(uid) || !Guid.TryParse(uid, out var userId) || userId == Guid.Empty ||
            string.IsNullOrWhiteSpace(sub) ||
            string.IsNullOrWhiteSpace(sid) || !Guid.TryParse(sid, out var sessionId) || sessionId == Guid.Empty)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token inválido: claims obrigatórias ausentes (uid/sub/sid).");
            return;
        }

        var email = context.User.FindFirst("email")?.Value ?? string.Empty;

        var userContext = new UserContext
        {
            UserId = userId,
            ExternalAuthId = sub,
            Email = email
        };

        _userScopeService.Set(userContext, sessionId);

        await next(context);
    }
}
