using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Codebymister.Infrastructure.Configurations.Auth;

public static class AuthConfiguration
{
    public static IServiceCollection AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["AppJwt:Issuer"]
            ?? throw new InvalidOperationException("AppJwt:Issuer não configurado.");
        var audience = configuration["AppJwt:Audience"]
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

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "sub",
                    RoleClaimType = "role",
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

        services.AddAuthorization();

        return services;
    }
}
