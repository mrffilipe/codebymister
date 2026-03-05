using Codebymister.Application.UseCases.Leads.Dtos;

namespace Codebymister.Application.UseCases.Leads.Commands.CreateLead;

public interface ICreateLead
{
    Task<LeadDto> ExecuteAsync(CreateLeadRequest request, CancellationToken cancellationToken = default);
}


