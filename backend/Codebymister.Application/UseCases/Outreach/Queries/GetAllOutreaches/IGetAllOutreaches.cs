using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Queries.GetAllOutreaches;

public interface IGetAllOutreaches
{
    Task<List<OutreachDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}
