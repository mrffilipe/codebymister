using Codebymister.Application.Services;
using Codebymister.Application.Services.Auth;
using Codebymister.Domain.Services;
using Codebymister.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codebymister.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = configuration;

        services.AddScoped<IUnitOfWork, Persistence.UnitOfWork>();
        services.AddScoped<IUserScopeService, UserScopeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAppTokenIssuer, AppTokenIssuer>();

        return services;
    }
}
