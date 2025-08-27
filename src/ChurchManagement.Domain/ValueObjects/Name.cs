namespace ChurchManagement.Domain.ValueObjects;

public record Name
{
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";

    public Name(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty or whitespace.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty or whitespace.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }

    public override string ToString() => FullName;
}