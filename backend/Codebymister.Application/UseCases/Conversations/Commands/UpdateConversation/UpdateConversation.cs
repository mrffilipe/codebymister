using Codebymister.Application.UseCases.Conversations.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Conversations.Commands.UpdateConversation;

public class UpdateConversation : IUpdateConversation
{
    private readonly IConversationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateConversation(IConversationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ConversationDto?> ExecuteAsync(Guid id, UpdateConversationRequest request, CancellationToken cancellationToken = default)
    {
        var conversation = await _repository.GetByIdAsync(id, cancellationToken);
        if (conversation == null)
            return null;

        conversation.Update(
            request.InterestLevel,
            request.Timing,
            request.Notes,
            request.NextStep,
            request.Status
        );

        _repository.Update(conversation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ConversationDto(
            conversation.Id,
            conversation.LeadId,
            conversation.Lead.Name,
            conversation.InterestLevel,
            conversation.Timing,
            conversation.Notes,
            conversation.NextStep,
            conversation.Status,
            conversation.CreatedAt,
            conversation.UpdatedAt
        );
    }
}



