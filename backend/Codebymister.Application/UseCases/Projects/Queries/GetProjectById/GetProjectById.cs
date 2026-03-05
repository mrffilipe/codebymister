using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Queries.GetProjectById;

public class GetProjectById : IGetProjectById
{
    private readonly IProjectQueries _queries;

    public GetProjectById(IProjectQueries queries)
    {
        _queries = queries;
    }

    public async Task<ProjectDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _queries.GetByIdAsync(id, cancellationToken);
    }
}


