using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Queries.GetLeadById;

public interface IGetLeadById
{
    Task<LeadDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}

