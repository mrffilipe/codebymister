using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Outreach.Dtos;

public record CreateOutreachRequest(
    Guid LeadId,
    OutreachChannel Channel,
    string Message,
    string? Notes = null
);
