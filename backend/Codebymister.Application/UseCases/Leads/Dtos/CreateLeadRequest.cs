using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Leads.Dtos;

public record CreateLeadRequest(
    string Name,
    string Segment,
    string City,
    string ProblemDescription,
    LeadPriority Priority,
    LeadSource Source,
    string? Website = null,
    string? Instagram = null,
    string? Phone = null
);
