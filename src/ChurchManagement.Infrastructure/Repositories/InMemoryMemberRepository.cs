using ChurchManagement.Domain.Entities;
using ChurchManagement.Domain.Repositories;

namespace ChurchManagement.Infrastructure.Repositories;

public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = new();

    public Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = _members.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(member);
    }

    public Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var member = _members.FirstOrDefault(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(member);
    }

    public Task<IEnumerable<Member>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        var activeMembers = _members.Where(m => m.IsActive).ToList();
        return Task.FromResult<IEnumerable<Member>>(activeMembers);
    }

    public Task<IEnumerable<Member>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<Member>>(_members.ToList());
    }

    public Task<Member> AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        _members.Add(member);
        return Task.FromResult(member);
    }

    public Task UpdateAsync(Member member, CancellationToken cancellationToken = default)
    {
        var existingMember = _members.FirstOrDefault(m => m.Id == member.Id);
        if (existingMember != null)
        {
            var index = _members.IndexOf(existingMember);
            _members[index] = member;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var member = _members.FirstOrDefault(m => m.Id == id);
        if (member != null)
        {
            _members.Remove(member);
        }
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exists = _members.Any(m => m.Id == id);
        return Task.FromResult(exists);
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_members.Count);
    }
}