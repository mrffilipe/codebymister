using Codebymister.Application.UseCases.Outreach.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Outreach.Commands.UpdateOutreach;

public class UpdateOutreach : IUpdateOutreach
{
    private readonly IOutreachRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOutreach(IOutreachRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OutreachDto?> ExecuteAsync(Guid id, UpdateOutreachRequest request, CancellationToken cancellationToken = default)
    {
        var outreach = await _repository.GetByIdAsync(id, cancellationToken);
        if (outreach == null)
            return null;

        outreach.MarkAsResponded(request.ResponseAt, request.ResponseStatus);
        
        if (request.FollowUpSent)
            outreach.MarkFollowUpSent();

        if (!string.IsNullOrWhiteSpace(request.Notes))
            outreach.UpdateNotes(request.Notes);

        _repository.Update(outreach);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OutreachDto(
            outreach.Id,
            outreach.LeadId,
            outreach.Lead.Name,
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



