using ChurchManagement.Application.Interfaces;
using ChurchManagement.Application.Services;
using ChurchManagement.Domain.Repositories;
using ChurchManagement.Domain.Services;
using ChurchManagement.Infrastructure.Repositories;
using ChurchManagement.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChurchManagement.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IMemberRepository, InMemoryMemberRepository>();

        // Register domain services
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IEmailService, EmailService>();

        // Register application services
        services.AddScoped<IMemberService, MemberService>();

        return services;
    }
}