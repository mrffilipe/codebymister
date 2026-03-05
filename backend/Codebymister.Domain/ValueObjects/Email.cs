using System.Text.RegularExpressions;

namespace Codebymister.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        email = email.Trim().ToLowerInvariant();

        if (!IsValid(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        return new Email(email);
    }

    private static bool IsValid(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        return emailRegex.IsMatch(email);
    }

    public bool Equals(Email? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => obj is Email email && Equals(email);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}
