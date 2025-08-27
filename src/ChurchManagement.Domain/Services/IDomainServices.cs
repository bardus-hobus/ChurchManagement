namespace ChurchManagement.Domain.Services;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<T>(T domainEvent, CancellationToken cancellationToken = default) where T : class;
    Task DispatchAllAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default);
}

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email, string firstName, CancellationToken cancellationToken = default);
    Task SendNotificationEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default);
}