namespace ChurchManagement.Domain.ValueObjects;

public sealed record Address(
    string Line1,
    string? Line2,
    string City,
    string State,
    string Postal,
    string Country
);