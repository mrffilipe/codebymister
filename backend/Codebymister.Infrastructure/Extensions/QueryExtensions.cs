using Codebymister.Application.Services;
using Codebymister.Infrastructure.Persistence.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Codebymister.Infrastructure.Extensions;

public static class QueryExtensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<ILeadQueries, LeadQueries>();
        services.AddScoped<IMaintenanceQueries, MaintenanceQueries>();
        services.AddScoped<IProjectQueries, ProjectQueries>();
        services.AddScoped<IProposalQueries, ProposalQueries>();
        services.AddScoped<IOutreachQueries, OutreachQueries>();
        services.AddScoped<IConversationQueries, ConversationQueries>();

        return services;
    }
}
