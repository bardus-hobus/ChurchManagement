using System.Text.RegularExpressions;

namespace ChurchManagement.Domain.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex PhoneRegex = new Regex(
        @"^[\+]?[(]?[\d\s\-\.\(\)]+$",
        RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty or whitespace.", nameof(value));

        var trimmedValue = value.Trim();
        
        if (!PhoneRegex.IsMatch(trimmedValue))
            throw new ArgumentException("Invalid phone number format.", nameof(value));

        if (trimmedValue.Length < 7 || trimmedValue.Length > 20)
            throw new ArgumentException("Phone number must be between 7 and 20 characters.", nameof(value));

        Value = trimmedValue;
    }

    public static implicit operator string(PhoneNumber phone) => phone.Value;
    public static implicit operator PhoneNumber(string value) => new(value);

    public override string ToString() => Value;
}