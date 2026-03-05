using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.UseCases.Outreach.Queries.GetOutreachById;

public interface IGetOutreachById
{
    Task<OutreachDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}
