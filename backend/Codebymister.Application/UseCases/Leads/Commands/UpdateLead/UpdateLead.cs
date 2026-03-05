using Codebymister.Application.UseCases.Leads.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Leads.Commands.UpdateLead;

public class UpdateLead : IUpdateLead
{
    private readonly ILeadRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLead(ILeadRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LeadDto?> ExecuteAsync(Guid id, UpdateLeadRequest request, CancellationToken cancellationToken = default)
    {
        var lead = await _repository.GetByIdAsync(id, cancellationToken);

        if (lead == null)
            return null;

        lead.Update(
            request.Name,
            request.Segment,
            request.City,
            request.ProblemDescription,
            request.Priority,
            request.Source,
            request.Website,
            request.Instagram,
            request.Phone
        );

        _repository.Update(lead);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LeadDto.FromEntity(lead);
    }
}


