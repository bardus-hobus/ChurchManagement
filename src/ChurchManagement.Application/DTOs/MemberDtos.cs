namespace ChurchManagement.Application.DTOs;

public record MemberDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    DateTime? DateOfBirth,
    DateTime MembershipDate,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateMemberDto(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    DateTime? DateOfBirth
);

public record UpdateMemberDto(
    string FirstName,
    string LastName,
    string Email,
    string? Phone
);