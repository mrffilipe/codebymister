using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Proposals.Dtos;

public record ProposalDto(
    Guid Id,
    Guid LeadId,
    string LeadName,
    ProjectType ProjectType,
    decimal ProposedValue,
    DateTime SentAt,
    ProposalStatus Status,
    string? RefusalReason,
    DateTime? ResponseAt,
    string? Notes
);
