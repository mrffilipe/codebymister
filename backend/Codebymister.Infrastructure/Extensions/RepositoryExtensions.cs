using Codebymister.Application.Services;
using Codebymister.Domain.Repositories;
using Codebymister.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Codebymister.Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IOutreachRepository, OutreachRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();

        return services;
    }
}
