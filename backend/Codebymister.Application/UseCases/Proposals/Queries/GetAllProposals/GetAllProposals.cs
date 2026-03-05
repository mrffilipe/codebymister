using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Proposals.Dtos;

namespace Codebymister.Application.UseCases.Proposals.Queries.GetAllProposals;

public class GetAllProposals : IGetAllProposals
{
    private readonly IProposalQueries _queries;

    public GetAllProposals(IProposalQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<ProposalDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}


