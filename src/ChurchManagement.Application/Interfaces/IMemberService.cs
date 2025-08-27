using ChurchManagement.Application.DTOs;

namespace ChurchManagement.Application.Interfaces;

public interface IMemberService
{
    Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default);
    Task<MemberDto> UpdateMemberAsync(Guid id, UpdateMemberDto updateMemberDto, CancellationToken cancellationToken = default);
    Task<MemberDto?> GetMemberByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<MemberDto?> GetMemberByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<MemberDto>> GetAllMembersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<MemberDto>> GetActiveMembersAsync(CancellationToken cancellationToken = default);
    Task ActivateMemberAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeactivateMemberAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteMemberAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> GetMemberCountAsync(CancellationToken cancellationToken = default);
}