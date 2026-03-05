using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Outreach.Commands.CreateOutreach;

public record CreateOutreachRequest(
    Guid LeadId,
    OutreachChannel Channel,
    string Message,
    string? Notes
);


