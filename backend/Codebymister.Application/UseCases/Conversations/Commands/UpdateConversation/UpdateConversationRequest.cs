using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Conversations.Commands.UpdateConversation;

public record UpdateConversationRequest(
    InterestLevel InterestLevel,
    Timing Timing,
    string Notes,
    string? NextStep,
    ConversationStatus Status
);


