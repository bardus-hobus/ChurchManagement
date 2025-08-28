using ChurchManagement.Domain.Common;
using ChurchManagement.Domain.Enums;
using ChurchManagement.Domain.ValueObjects;

namespace ChurchManagement.Domain.Entities;

public class Member : Entity
{
    private readonly List<string> _groups = [];
    private readonly List<PermissionType> _permissions = [];
    private readonly List<Member> _children = [];

    public Name Name { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DateOnly MembershipDate { get; private set; }
    public DateOnly? BaptizedDate { get; private set; }
    public Member? Spouse { get; private set; }
    public IReadOnlyList<Member> Children => _children.AsReadOnly();
    public Gender Gender { get; private set; }
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public IReadOnlyList<string> Groups => _groups.AsReadOnly();
    public IReadOnlyList<PermissionType> Permissions => _permissions.AsReadOnly();

    // Constructor for creating new members
    public Member(
        string firstName,
        string lastName,
        DateOnly birthDate,
        DateOnly membershipDate,
        Gender gender)
    {
        Name = new Name(firstName, lastName);
        BirthDate = birthDate;
        MembershipDate = membershipDate;
        Gender = gender;

        // Default permission for new members
        _permissions.Add(PermissionType.Member);
    }

    // Constructor for entity reconstitution (e.g., from database)
    public Member(
        Guid id,
        string firstName,
        string lastName,
        DateOnly birthDate,
        DateOnly membershipDate,
        Gender gender,
        DateOnly? baptizedDate = null,
        Email? email = null,
        PhoneNumber? phoneNumber = null) : base(id)
    {
        Name = new Name(firstName, lastName);
        BirthDate = birthDate;
        MembershipDate = membershipDate;
        Gender = gender;
        BaptizedDate = baptizedDate;
        Email = email;
        PhoneNumber = phoneNumber;

        // Default permission for reconstituted members
        _permissions.Add(PermissionType.Member);
    }

    public void UpdateName(string firstName, string lastName)
    {
        Name = new Name(firstName, lastName);
        UpdateTimestamp();
    }

    public void UpdateBirthDate(DateOnly birthDate)
    {
        BirthDate = birthDate;
        UpdateTimestamp();
    }

    public void UpdateGender(Gender gender)
    {
        Gender = gender;
        UpdateTimestamp();
    }

    public void SetBaptizedDate(DateOnly baptizedDate)
    {
        BaptizedDate = baptizedDate;
        UpdateTimestamp();
    }

    public void RemoveBaptizedDate()
    {
        BaptizedDate = null;
        UpdateTimestamp();
    }

    public void SetEmail(Email? email)
    {
        Email = email;
        UpdateTimestamp();
    }

    public void SetPhoneNumber(PhoneNumber? phoneNumber)
    {
        PhoneNumber = phoneNumber;
        UpdateTimestamp();
    }

    public void SetSpouse(Member? spouse)
    {
        if (spouse != null && spouse.Gender == Gender)
            throw new InvalidOperationException("Spouse must be of the opposite gender.");

        // Remove existing spouse relationship
        if (Spouse != null && Spouse.Spouse == this)
        {
            Spouse.Spouse = null;
            Spouse.UpdateTimestamp();
        }

        Spouse = spouse;

        // Set reciprocal relationship
        if (spouse != null && spouse.Spouse != this)
        {
            spouse.Spouse = this;
            spouse.UpdateTimestamp();
        }

        UpdateTimestamp();
    }

    public void AddChild(Member child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));

        if (_children.Contains(child))
            return;

        _children.Add(child);
        UpdateTimestamp();
    }

    public void RemoveChild(Member child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));

        _children.Remove(child);
        UpdateTimestamp();
    }

    public void AddToGroup(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Group name cannot be empty or whitespace.", nameof(groupName));

        var trimmedGroupName = groupName.Trim();
        if (!_groups.Contains(trimmedGroupName, StringComparer.OrdinalIgnoreCase))
        {
            _groups.Add(trimmedGroupName);
            UpdateTimestamp();
        }
    }

    public void RemoveFromGroup(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            return;

        var itemToRemove = _groups.FirstOrDefault(g =>
            string.Equals(g, groupName.Trim(), StringComparison.OrdinalIgnoreCase));

        if (itemToRemove != null)
        {
            _groups.Remove(itemToRemove);
            UpdateTimestamp();
        }
    }

    public void AddPermission(PermissionType permission)
    {
        if (!_permissions.Contains(permission))
        {
            _permissions.Add(permission);
            UpdateTimestamp();
        }
    }

    public void RemovePermission(PermissionType permission)
    {
        if (_permissions.Contains(permission))
        {
            _permissions.Remove(permission);
            UpdateTimestamp();
        }
    }

    public bool HasPermission(PermissionType permission)
    {
        return _permissions.Contains(permission);
    }

    public bool IsInGroup(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            return false;

        return _groups.Any(g => string.Equals(g, groupName.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}