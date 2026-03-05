using Codebymister.Domain.Common;
using Codebymister.Domain.Enums;
using Codebymister.Domain.ValueObjects;

namespace Codebymister.Domain.Entities;

public class Lead : BaseEntity
{
    public string Name { get; private set; }
    public string Segment { get; private set; }
    public string City { get; private set; }
    public Url? Website { get; private set; }
    public Instagram? Instagram { get; private set; }
    public Phone? Phone { get; private set; }
    public string ProblemDescription { get; private set; }
    public LeadPriority Priority { get; private set; }
    public LeadSource Source { get; private set; }

    private Lead() { }

    public Lead(
        string name,
        string segment,
        string city,
        string problemDescription,
        LeadPriority priority,
        LeadSource source,
        string? website = null,
        string? instagram = null,
        string? phone = null)
    {
        Name = name;
        Segment = segment;
        City = city;
        Website = !string.IsNullOrWhiteSpace(website) ? Url.Create(website) : null;
        Instagram = !string.IsNullOrWhiteSpace(instagram) ? ValueObjects.Instagram.Create(instagram) : null;
        Phone = !string.IsNullOrWhiteSpace(phone) ? ValueObjects.Phone.Create(phone) : null;
        ProblemDescription = problemDescription;
        Priority = priority;
        Source = source;
    }

    public void Update(
        string name,
        string segment,
        string city,
        string problemDescription,
        LeadPriority priority,
        LeadSource source,
        string? website,
        string? instagram,
        string? phone)
    {
        Name = name;
        Segment = segment;
        City = city;
        Website = !string.IsNullOrWhiteSpace(website) ? Url.Create(website) : null;
        Instagram = !string.IsNullOrWhiteSpace(instagram) ? ValueObjects.Instagram.Create(instagram) : null;
        Phone = !string.IsNullOrWhiteSpace(phone) ? ValueObjects.Phone.Create(phone) : null;
        ProblemDescription = problemDescription;
        Priority = priority;
        Source = source;
    }
}
