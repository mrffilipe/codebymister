using Codebymister.Application.UseCases.Leads.Dtos;
using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Leads.Commands.CreateLead;

public class CreateLead : ICreateLead
{
    private readonly ILeadRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLead(ILeadRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LeadDto> ExecuteAsync(CreateLeadRequest request, CancellationToken cancellationToken = default)
    {
        var lead = new Lead(
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

        await _repository.AddAsync(lead, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LeadDto.FromEntity(lead);
    }
}


