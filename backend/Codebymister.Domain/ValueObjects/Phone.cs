using System.Text.RegularExpressions;

namespace Codebymister.Domain.ValueObjects;

public sealed class Phone : IEquatable<Phone>
{
    public string Value { get; }

    private Phone(string value)
    {
        Value = value;
    }

    public static Phone Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone cannot be empty", nameof(phone));

        var digitsOnly = Regex.Replace(phone, @"\D", "");

        if (digitsOnly.Length < 10 || digitsOnly.Length > 11)
            throw new ArgumentException("Phone must have 10 or 11 digits", nameof(phone));

        return new Phone(digitsOnly);
    }

    public string Formatted()
    {
        if (Value.Length == 11)
            return $"({Value.Substring(0, 2)}) {Value.Substring(2, 5)}-{Value.Substring(7)}";
        
        return $"({Value.Substring(0, 2)}) {Value.Substring(2, 4)}-{Value.Substring(6)}";
    }

    public bool Equals(Phone? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => obj is Phone phone && Equals(phone);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(Phone phone) => phone.Value;
}
