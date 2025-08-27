using ChurchManagement.Application.DTOs;

namespace ChurchManagement.Application.Queries;

public record GetMemberByIdQuery(Guid Id);

public record GetMemberByEmailQuery(string Email);

public record GetAllMembersQuery;

public record GetActiveMembersQuery;

public record GetMemberStatisticsQuery;