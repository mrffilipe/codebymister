using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Queries.GetAllOutreaches;

public class GetAllOutreaches : IGetAllOutreaches
{
    private readonly IOutreachQueries _queries;

    public GetAllOutreaches(IOutreachQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<OutreachDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}

