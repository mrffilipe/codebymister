using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Queries.GetProposalById;

public class GetProposalById : IGetProposalById
{
    private readonly IProposalQueries _queries;

    public GetProposalById(IProposalQueries queries)
    {
        _queries = queries;
    }

    public async Task<ProposalDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}


