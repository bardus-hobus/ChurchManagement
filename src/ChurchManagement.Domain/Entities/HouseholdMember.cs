using ChurchManagement.Domain.Enums;

namespace ChurchManagement.Domain.Entities;

public class HouseholdMember
{
    private HouseholdMember() { } // EF Core

    internal HouseholdMember(Guid householdId, Guid memberId, HouseholdRole role, DateOnly since)
    {
        HouseholdId = householdId;
        MemberId = memberId;
        Role = role;
        Since = since;
        Active = true;
    }

    public Guid HouseholdId { get; private set; }
    public Guid MemberId { get; private set; }
    public HouseholdRole Role { get; private set; }
    public DateOnly Since { get; private set; }
    public DateOnly? Until { get; private set; }
    public bool Active { get; private set; }

    internal void End(DateOnly until)
    {
        Until = until;
        Active = false;
    }
}