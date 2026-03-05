using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Commands.UpdateLead;

public interface IUpdateLead
{
    Task<LeadDto?> ExecuteAsync(Guid id, UpdateLeadRequest request, CancellationToken cancellationToken = default);
}


