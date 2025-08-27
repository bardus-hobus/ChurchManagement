using ChurchManagement.Domain.Common;

namespace ChurchManagement.Domain.Entities;

public class Member : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public DateTime MembershipDate { get; private set; }
    public bool IsActive { get; private set; }

    private Member() : base()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public Member(string firstName, string lastName, string email, string? phone = null, DateTime? dateOfBirth = null)
        : base()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        DateOfBirth = dateOfBirth;
        MembershipDate = DateTime.UtcNow;
        IsActive = true;
    }

    public void UpdatePersonalInfo(string firstName, string lastName, string email, string? phone = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }
}