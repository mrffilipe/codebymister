using Codebymister.Application.Common;
using Codebymister.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Codebymister.Infrastructure.Services;

public sealed class AppTokenIssuer : IAppTokenIssuer
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly SymmetricSecurityKey _key;
    private readonly int _accessTokenMinutes;

    public AppTokenIssuer(IConfiguration configuration)
    {
        _issuer = configuration["AppJwt:Issuer"]
            ?? throw new InvalidOperationException("AppJwt:Issuer não configurado.");
        _audience = configuration["AppJwt:Audience"]
            ?? throw new InvalidOperationException("AppJwt:Audience não configurado.");

        var secretB64 = configuration["AppJwt:Key"]
            ?? throw new InvalidOperationException("AppJwt:Key não configurado.");

        byte[] keyBytes;
        try
        {
            keyBytes = Convert.FromBase64String(secretB64);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("AppJwt:Key não é um Base64 válido.", ex);
        }

        if (keyBytes.Length < 32)
            throw new InvalidOperationException("AppJwt:Key deve ter pelo menos 32 bytes (Base64).");

        _key = new SymmetricSecurityKey(keyBytes);
        _accessTokenMinutes = int.TryParse(configuration["AppJwt:AccessTokenMinutes"], out var minutes)
            ? minutes
            : 60;
    }

    public string Issue(AppTokenClaims claims)
    {
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var jwtClaims = new List<Claim>
        {
            new("uid", claims.UserId.ToString()),
            new("sub", claims.ExternalAuthId),
            new("sid", claims.SessionId.ToString())
        };

        if (!string.IsNullOrWhiteSpace(claims.Email))
            jwtClaims.Add(new Claim("email", claims.Email));

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: jwtClaims,
            notBefore: DateTime.UtcNow.AddSeconds(-5),
            expires: DateTime.UtcNow.AddMinutes(_accessTokenMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
