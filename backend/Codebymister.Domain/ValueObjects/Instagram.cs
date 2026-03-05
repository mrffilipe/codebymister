using System.Text.RegularExpressions;

namespace Codebymister.Domain.ValueObjects;

public sealed class Instagram : IEquatable<Instagram>
{
    public string Value { get; }

    private Instagram(string value)
    {
        Value = value;
    }

    public static Instagram Create(string instagram)
    {
        if (string.IsNullOrWhiteSpace(instagram))
            throw new ArgumentException("Instagram cannot be empty", nameof(instagram));

        instagram = instagram.Trim();

        if (instagram.StartsWith("@"))
            instagram = instagram.Substring(1);

        if (instagram.Contains("instagram.com/"))
        {
            var match = Regex.Match(instagram, @"instagram\.com/([a-zA-Z0-9._]+)");
            if (match.Success)
                instagram = match.Groups[1].Value;
        }

        instagram = instagram.TrimEnd('/');

        if (!Regex.IsMatch(instagram, @"^[a-zA-Z0-9._]+$"))
            throw new ArgumentException("Invalid Instagram username format", nameof(instagram));

        return new Instagram(instagram);
    }

    public string WithAt() => $"@{Value}";
    public string AsUrl() => $"https://instagram.com/{Value}";

    public bool Equals(Instagram? other)
    {
        if (other is null) return false;
        return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => obj is Instagram instagram && Equals(instagram);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(Instagram instagram) => instagram.Value;
}
