using Codebymister.Domain.Enums;

namespace Codebymister.Application.UseCases.Leads.Dtos;

public record LeadDto(
    Guid Id,
    string Name,
    string Segment,
    string City,
    string? Website,
    string? Instagram,
    string? Phone,
    string ProblemDescription,
    LeadPriority Priority,
    LeadSource Source,
    DateTime CreatedAt,
    DateTime? UpdatedAt
)
{
    public static LeadDto FromEntity(Domain.Entities.Lead lead)
    {
        return new LeadDto(
            lead.Id,
            lead.Name,
            lead.Segment,
            lead.City,
            lead.Website?.Value,
            lead.Instagram?.Value,
            lead.Phone?.Value,
            lead.ProblemDescription,
            lead.Priority,
            lead.Source,
            lead.CreatedAt,
            lead.UpdatedAt
        );
    }
};
