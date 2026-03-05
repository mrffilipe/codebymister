using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.Services;

public interface ILeadQueries
{
    Task<LeadDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LeadDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
