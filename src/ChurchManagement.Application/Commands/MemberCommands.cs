using ChurchManagement.Application.DTOs;

namespace ChurchManagement.Application.Commands;

public record CreateMemberCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    DateTime? DateOfBirth
);

public record UpdateMemberCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Phone
);

public record ActivateMemberCommand(Guid MemberId);

public record DeactivateMemberCommand(Guid MemberId);

public record DeleteMemberCommand(Guid MemberId);