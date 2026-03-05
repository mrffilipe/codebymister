using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Queries.GetAllLeads;

public interface IGetAllLeads
{
    Task<List<LeadDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}

