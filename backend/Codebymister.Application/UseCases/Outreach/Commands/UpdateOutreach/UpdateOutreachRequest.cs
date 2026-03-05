using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Outreach.Commands.UpdateOutreach;

public record UpdateOutreachRequest(
    DateTime? ResponseAt,
    ResponseStatus ResponseStatus,
    bool FollowUpSent,
    string? Notes
);


