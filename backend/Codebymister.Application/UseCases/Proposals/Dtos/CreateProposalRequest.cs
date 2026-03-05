using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Proposals.Dtos;

public record CreateProposalRequest(
    Guid LeadId,
    ProjectType ProjectType,
    decimal ProposedValue,
    string? Notes = null
);
