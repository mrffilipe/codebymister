using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Queries.GetAllLeads;

public class GetAllLeads : IGetAllLeads
{
    private readonly ILeadQueries _queries;

    public GetAllLeads(ILeadQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<LeadDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}

