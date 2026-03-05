using Codebymister.Application.UseCases.Conversations.Commands.CreateConversation;
using Codebymister.Application.UseCases.Conversations.Commands.DeleteConversation;
using Codebymister.Application.UseCases.Conversations.Commands.UpdateConversation;
using Codebymister.Application.UseCases.Conversations.Queries.GetAllConversations;
using Codebymister.Application.UseCases.Conversations.Queries.GetConversationById;
using Codebymister.Application.UseCases.Dashboard.GetDashboardData;
using Codebymister.Application.UseCases.Leads.Commands.CreateLead;
using Codebymister.Application.UseCases.Leads.Commands.DeleteLead;
using Codebymister.Application.UseCases.Leads.Commands.UpdateLead;
using Codebymister.Application.UseCases.Leads.Queries.GetAllLeads;
using Codebymister.Application.UseCases.Leads.Queries.GetLeadById;
using Codebymister.Application.UseCases.Maintenance.Commands.CreateMaintenance;
using Codebymister.Application.UseCases.Maintenance.Commands.DeleteMaintenance;
using Codebymister.Application.UseCases.Maintenance.Commands.UpdateMaintenance;
using Codebymister.Application.UseCases.Maintenance.Queries.GetAllMaintenances;
using Codebymister.Application.UseCases.Maintenance.Queries.GetMaintenanceById;
using Codebymister.Application.UseCases.Outreach.Commands.CreateOutreach;
using Codebymister.Application.UseCases.Outreach.Commands.DeleteOutreach;
using Codebymister.Application.UseCases.Outreach.Commands.UpdateOutreach;
using Codebymister.Application.UseCases.Outreach.Queries.GetAllOutreaches;
using Codebymister.Application.UseCases.Outreach.Queries.GetOutreachById;
using Codebymister.Application.UseCases.Projects.Commands.CreateProject;
using Codebymister.Application.UseCases.Projects.Commands.DeleteProject;
using Codebymister.Application.UseCases.Projects.Commands.UpdateProject;
using Codebymister.Application.UseCases.Projects.Queries.GetAllProjects;
using Codebymister.Application.UseCases.Projects.Queries.GetProjectById;
using Codebymister.Application.UseCases.Proposals.Commands.CreateProposal;
using Codebymister.Application.UseCases.Proposals.Commands.DeleteProposal;
using Codebymister.Application.UseCases.Proposals.Commands.UpdateProposal;
using Codebymister.Application.UseCases.Proposals.Queries.GetAllProposals;
using Codebymister.Application.UseCases.Proposals.Queries.GetProposalById;
using Microsoft.Extensions.DependencyInjection;

namespace Codebymister.Infrastructure.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateLead, CreateLead>();
        services.AddScoped<IGetAllLeads, GetAllLeads>();
        services.AddScoped<IGetLeadById, GetLeadById>();
        services.AddScoped<IUpdateLead, UpdateLead>();
        services.AddScoped<IDeleteLead, DeleteLead>();

        services.AddScoped<ICreateOutreach, CreateOutreach>();
        services.AddScoped<IGetAllOutreaches, GetAllOutreaches>();
        services.AddScoped<IGetOutreachById, GetOutreachById>();
        services.AddScoped<IUpdateOutreach, UpdateOutreach>();
        services.AddScoped<IDeleteOutreach, DeleteOutreach>();

        services.AddScoped<ICreateConversation, CreateConversation>();
        services.AddScoped<IGetAllConversations, GetAllConversations>();
        services.AddScoped<IGetConversationById, GetConversationById>();
        services.AddScoped<IUpdateConversation, UpdateConversation>();
        services.AddScoped<IDeleteConversation, DeleteConversation>();

        services.AddScoped<ICreateProposal, CreateProposal>();
        services.AddScoped<IGetAllProposals, GetAllProposals>();
        services.AddScoped<IGetProposalById, GetProposalById>();
        services.AddScoped<IUpdateProposal, UpdateProposal>();
        services.AddScoped<IDeleteProposal, DeleteProposal>();

        services.AddScoped<ICreateProject, CreateProject>();
        services.AddScoped<IGetAllProjects, GetAllProjects>();
        services.AddScoped<IGetProjectById, GetProjectById>();
        services.AddScoped<IUpdateProject, UpdateProject>();
        services.AddScoped<IDeleteProject, DeleteProject>();

        services.AddScoped<ICreateMaintenance, CreateMaintenance>();
        services.AddScoped<IGetAllMaintenances, GetAllMaintenances>();
        services.AddScoped<IGetMaintenanceById, GetMaintenanceById>();
        services.AddScoped<IUpdateMaintenance, UpdateMaintenance>();
        services.AddScoped<IDeleteMaintenance, DeleteMaintenance>();

        services.AddScoped<IGetDashboardData, GetDashboardData>();

        return services;
    }
}
