using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Conversations.Dtos;

public record CreateConversationRequest(
    Guid LeadId,
    InterestLevel InterestLevel,
    Timing Timing,
    string Notes,
    string? NextStep = null
);
