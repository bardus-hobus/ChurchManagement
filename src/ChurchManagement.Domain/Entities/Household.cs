using ChurchManagement.Domain.Enums;
using ChurchManagement.Domain.ValueObjects;

namespace ChurchManagement.Domain.Entities;

public class Household
{
    private readonly List<HouseholdMember> _members = new();

    private Household() { } // EF Core

    public Household(Address address)
    {
        Id = Guid.NewGuid();
        Address = address ?? throw new ArgumentNullException(nameof(address));
        DisplayName = string.Empty;
    }

    public Guid Id { get; private set; }
    public string DisplayName { get; private set; }
    public Address Address { get; private set; }

    public IReadOnlyList<HouseholdMember> Members => _members.AsReadOnly();

    public void AddMember(Member member, HouseholdRole role, DateOnly since)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (_members.Exists(m => m.MemberId == member.Id && m.Active))
            throw new InvalidOperationException("Member already in household.");

        _members.Add(new HouseholdMember(Id, member.Id, role, since));
        RecalculateDisplayName();
    }

    public void EndMembership(Guid memberId, DateOnly until)
    {
        var link = _members.Find(m => m.MemberId == memberId && m.Active)
                   ?? throw new InvalidOperationException("Member not active in this household.");

        link.End(until);
        RecalculateDisplayName();
    }

    private void RecalculateDisplayName()
    {
        // TODO: choose how to format display name (e.g. "Smith Family")
        DisplayName = $"Household {Id}";
    }
}