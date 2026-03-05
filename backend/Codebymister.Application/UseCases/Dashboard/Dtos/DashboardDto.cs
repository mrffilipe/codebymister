namespace Codebymister.Application.UseCases.Dashboard.Dtos;

public record DashboardDto(
    int TotalLeads,
    int TotalOutreach,
    int TotalResponses,
    int TotalProposals,
    int ClosedProjects,
    decimal ConversionRate,
    decimal AverageTicket,
    decimal MonthlyRevenue,
    decimal RecurringRevenue,
    decimal ForecastRevenue
);
