using Codebymister.Application.UseCases.Outreach.Dtos;

namespace Codebymister.Application.Services;

public interface IOutreachQueries
{
    Task<OutreachDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<OutreachDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
