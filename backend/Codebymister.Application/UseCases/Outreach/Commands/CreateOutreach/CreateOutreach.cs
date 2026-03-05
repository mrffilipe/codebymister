using Codebymister.Application.UseCases.Outreach.Dtos;
using Codebymister.Domain.Entities;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Outreach.Commands.CreateOutreach;

public class CreateOutreach : ICreateOutreach
{
    private readonly ILeadRepository _leadRepository;
    private readonly IOutreachRepository _outreachRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOutreach(
        ILeadRepository leadRepository,
        IOutreachRepository outreachRepository,
        IUnitOfWork unitOfWork)
    {
        _leadRepository = leadRepository;
        _outreachRepository = outreachRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutreachDto> ExecuteAsync(CreateOutreachRequest request, CancellationToken cancellationToken = default)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException("Lead not found");

        var outreach = new Domain.Entities.Outreach(
            request.LeadId,
            request.Channel,
            request.Message,
            request.Notes
        );

        await _outreachRepository.AddAsync(outreach, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OutreachDto(
            outreach.Id,
            outreach.LeadId,
            lead.Name,
            outreach.Channel,
            outreach.Message,
            outreach.SentAt,
            outreach.Responded,
            outreach.ResponseAt,
            outreach.ResponseStatus,
            outreach.FollowUpSent,
            outreach.Notes
        );
    }
}



