using Codebymister.API.Common;
using Codebymister.Application.Common;
using Codebymister.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebymister.API.Controllers;

public class AuthController : V1BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("exchange-token")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthSession>> ExchangeToken([FromBody] ExchangeTokenRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _authService.ExchangeTokenAsync(request, cancellationToken));
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<AuthSession>> Refresh(CancellationToken cancellationToken)
    {
        return Ok(await _authService.RefreshAsync(cancellationToken));
    }
}
