using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Queries.GetLeadById;

public class GetLeadById : IGetLeadById
{
    private readonly ILeadQueries _queries;

    public GetLeadById(ILeadQueries queries)
    {
        _queries = queries;
    }

    public async Task<LeadDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}

