using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Outreach.Dtos;

public record MarkRespondedRequest(
    ResponseStatus Status,
    string? Notes = null
);
