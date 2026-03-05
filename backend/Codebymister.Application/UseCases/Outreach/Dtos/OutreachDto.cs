using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Outreach.Dtos;

public record OutreachDto(
    Guid Id,
    Guid LeadId,
    string LeadName,
    OutreachChannel Channel,
    string Message,
    DateTime SentAt,
    bool Responded,
    DateTime? ResponseAt,
    ResponseStatus ResponseStatus,
    bool FollowUpSent,
    string? Notes
);
