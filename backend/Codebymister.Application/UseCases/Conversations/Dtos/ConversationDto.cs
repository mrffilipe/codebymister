using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Conversations.Dtos;

public record ConversationDto(
    Guid Id,
    Guid LeadId,
    string LeadName,
    InterestLevel InterestLevel,
    Timing Timing,
    string Notes,
    string? NextStep,
    ConversationStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
