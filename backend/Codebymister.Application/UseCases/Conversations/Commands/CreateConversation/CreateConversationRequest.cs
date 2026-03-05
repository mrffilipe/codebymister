using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Conversations.Commands.CreateConversation;

public record CreateConversationRequest(
    Guid LeadId,
    InterestLevel InterestLevel,
    Timing Timing,
    string Notes,
    string? NextStep
);


