using Codebymister.Application.UseCases.Conversations.Dtos;
using Codebymister.Domain.Repositories;
using Codebymister.Domain.Services;

namespace Codebymister.Application.UseCases.Conversations.Commands.CreateConversation;

public class CreateConversation : ICreateConversation
{
    private readonly ILeadRepository _leadRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateConversation(
        ILeadRepository leadRepository,
        IConversationRepository conversationRepository,
        IUnitOfWork unitOfWork)
    {
        _leadRepository = leadRepository;
        _conversationRepository = conversationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ConversationDto> ExecuteAsync(CreateConversationRequest request, CancellationToken cancellationToken = default)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException("Lead not found");

        var conversation = new Domain.Entities.Conversation(
            request.LeadId,
            request.InterestLevel,
            request.Timing,
            request.Notes,
            request.NextStep
        );

        await _conversationRepository.AddAsync(conversation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ConversationDto(
            conversation.Id,
            conversation.LeadId,
            lead.Name,
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



