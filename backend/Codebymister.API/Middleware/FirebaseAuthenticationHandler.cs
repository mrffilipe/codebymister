using System.Security.Claims;
using System.Text.Encodings.Web;
using Codebymister.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Codebymister.API.Middleware;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseAuthService _firebaseAuthService;

    public FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        FirebaseAuthService firebaseAuthService) : base(options, logger, encoder)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.NoResult();
        }

        string? bearerToken = Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
        {
            return AuthenticateResult.Fail("Invalid authorization header");
        }

        string token = bearerToken.Substring("Bearer ".Length);

        try
        {
            var firebaseToken = await _firebaseAuthService.VerifyTokenAsync(token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, firebaseToken.Uid),
                new Claim(ClaimTypes.Email, firebaseToken.Claims.GetValueOrDefault("email")?.ToString() ?? ""),
                new Claim(ClaimTypes.Name, firebaseToken.Claims.GetValueOrDefault("name")?.ToString() ?? "")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Token validation failed: {ex.Message}");
        }
    }
}
