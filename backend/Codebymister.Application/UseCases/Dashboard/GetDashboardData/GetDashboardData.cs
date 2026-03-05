using Codebymister.Application.UseCases.Dashboard.Dtos;
using Codebymister.Domain.Enums;
using Codebymister.Domain.Repositories;

namespace Codebymister.Application.UseCases.Dashboard.GetDashboardData;

public class GetDashboardData : IGetDashboardData
{
    private readonly ILeadRepository _leadRepository;
    private readonly IOutreachRepository _outreachRepository;
    private readonly IProposalRepository _proposalRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMaintenanceRepository _maintenanceRepository;

    public GetDashboardData(
        ILeadRepository leadRepository,
        IOutreachRepository outreachRepository,
        IProposalRepository proposalRepository,
        IProjectRepository projectRepository,
        IMaintenanceRepository maintenanceRepository)
    {
        _leadRepository = leadRepository;
        _outreachRepository = outreachRepository;
        _proposalRepository = proposalRepository;
        _projectRepository = projectRepository;
        _maintenanceRepository = maintenanceRepository;
    }

    public async Task<DashboardDto> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var leads = await _leadRepository.GetAllAsync(cancellationToken);
        var outreaches = await _outreachRepository.GetAllAsync(cancellationToken);
        var proposals = await _proposalRepository.GetAllAsync(cancellationToken);
        var projects = await _projectRepository.GetAllAsync(cancellationToken);
        var maintenances = await _maintenanceRepository.GetAllAsync(cancellationToken);

        var totalLeads = leads.Count;
        var totalOutreach = outreaches.Count;
        var totalResponses = outreaches.Count(o => o.Responded);
        var totalProposals = proposals.Count;
        var closedProjects = projects.Count(p => p.Status == ProjectStatus.Completed || p.Status == ProjectStatus.Delivered);

        var conversionRate = totalLeads > 0 ? (decimal)closedProjects / totalLeads * 100 : 0;
        
        var completedProjects = projects.Where(p => p.Status == ProjectStatus.Completed || p.Status == ProjectStatus.Delivered).ToList();
        var averageTicket = completedProjects.Any() ? completedProjects.Average(p => p.ClosedValue) : 0;

        var currentMonth = DateTime.UtcNow.Month;
        var currentYear = DateTime.UtcNow.Year;
        var monthlyRevenue = projects
            .Where(p => p.CreatedAt.Month == currentMonth && p.CreatedAt.Year == currentYear)
            .Sum(p => p.ClosedValue);

        var recurringRevenue = maintenances
            .Where(m => m.Status == MaintenanceStatus.Active)
            .Sum(m => m.MonthlyValue);

        var forecastRevenue = proposals
            .Where(p => p.Status == ProposalStatus.Sent || p.Status == ProposalStatus.UnderReview)
            .Sum(p => p.ProposedValue);

        return new DashboardDto(
            totalLeads,
            totalOutreach,
            totalResponses,
            totalProposals,
            closedProjects,
            conversionRate,
            averageTicket,
            monthlyRevenue,
            recurringRevenue,
            forecastRevenue
        );
    }
}
