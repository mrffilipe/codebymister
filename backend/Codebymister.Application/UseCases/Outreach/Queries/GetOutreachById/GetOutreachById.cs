using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Queries.GetOutreachById;

public class GetOutreachById : IGetOutreachById
{
    private readonly IOutreachQueries _queries;

    public GetOutreachById(IOutreachQueries queries)
    {
        _queries = queries;
    }

    public async Task<OutreachDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}


