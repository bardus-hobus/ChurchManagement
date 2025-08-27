using ChurchManagement.Application.DTOs;
using ChurchManagement.Application.Interfaces;
using ChurchManagement.Domain.Entities;
using ChurchManagement.Domain.Repositories;

namespace ChurchManagement.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto, CancellationToken cancellationToken = default)
    {
        var member = new Member(
            createMemberDto.FirstName,
            createMemberDto.LastName,
            createMemberDto.Email,
            createMemberDto.Phone,
            createMemberDto.DateOfBirth
        );

        var createdMember = await _memberRepository.AddAsync(member, cancellationToken);
        return MapToDto(createdMember);
    }

    public async Task<MemberDto> UpdateMemberAsync(Guid id, UpdateMemberDto updateMemberDto, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
        if (member == null)
            throw new ArgumentException($"Member with ID {id} not found");

        member.UpdatePersonalInfo(
            updateMemberDto.FirstName,
            updateMemberDto.LastName,
            updateMemberDto.Email,
            updateMemberDto.Phone
        );

        await _memberRepository.UpdateAsync(member, cancellationToken);
        return MapToDto(member);
    }

    public async Task<MemberDto?> GetMemberByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
        return member != null ? MapToDto(member) : null;
    }

    public async Task<MemberDto?> GetMemberByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByEmailAsync(email, cancellationToken);
        return member != null ? MapToDto(member) : null;
    }

    public async Task<IEnumerable<MemberDto>> GetAllMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken);
        return members.Select(MapToDto);
    }

    public async Task<IEnumerable<MemberDto>> GetActiveMembersAsync(CancellationToken cancellationToken = default)
    {
        var members = await _memberRepository.GetAllActiveAsync(cancellationToken);
        return members.Select(MapToDto);
    }

    public async Task ActivateMemberAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
        if (member == null)
            throw new ArgumentException($"Member with ID {id} not found");

        member.Activate();
        await _memberRepository.UpdateAsync(member, cancellationToken);
    }

    public async Task DeactivateMemberAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
        if (member == null)
            throw new ArgumentException($"Member with ID {id} not found");

        member.Deactivate();
        await _memberRepository.UpdateAsync(member, cancellationToken);
    }

    public async Task DeleteMemberAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _memberRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<int> GetMemberCountAsync(CancellationToken cancellationToken = default)
    {
        return await _memberRepository.GetTotalCountAsync(cancellationToken);
    }

    private static MemberDto MapToDto(Member member)
    {
        return new MemberDto(
            member.Id,
            member.FirstName,
            member.LastName,
            member.Email,
            member.Phone,
            member.DateOfBirth,
            member.MembershipDate,
            member.IsActive,
            member.CreatedAt,
            member.UpdatedAt
        );
    }
}