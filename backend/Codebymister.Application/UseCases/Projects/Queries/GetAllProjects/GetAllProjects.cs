using Codebymister.Application.Services;
using Codebymister.Application.UseCases.Projects.Dtos;

namespace Codebymister.Application.UseCases.Projects.Queries.GetAllProjects;

public class GetAllProjects : IGetAllProjects
{
    private readonly IProjectQueries _queries;

    public GetAllProjects(IProjectQueries queries)
    {
        _queries = queries;
    }

    public async Task<List<ProjectDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _queries.GetAllAsync(cancellationToken);
    }
}


