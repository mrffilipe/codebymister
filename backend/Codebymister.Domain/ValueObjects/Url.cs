namespace Codebymister.Domain.ValueObjects;

public sealed class Url : IEquatable<Url>
{
    public string Value { get; }

    private Url(string value)
    {
        Value = value;
    }

    public static Url Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be empty", nameof(url));

        url = url.Trim();

        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            url = "https://" + url;

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || 
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("Invalid URL format", nameof(url));
        }

        return new Url(uri.ToString());
    }

    public bool Equals(Url? other)
    {
        if (other is null) return false;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => obj is Url url && Equals(url);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static implicit operator string(Url url) => url.Value;
}
