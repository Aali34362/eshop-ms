namespace BaseIntegrated.BaseEvents;

public abstract class BaseEventHandler<INotification, TRepository>
    (ILogger logger, TRepository repository)
    : INotificationHandler<INotification>
    where INotification : IBaseDomainEvent
{
    protected readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    protected readonly TRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    protected async Task ExecuteWithRetryPolicy(Func<Task> action)
    {
        var retryPolicy = Policy
            .Handle<DbUpdateException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(3,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                (exception, timespan, context) =>
                {
                    _logger.LogWarning("Retry due to {ExceptionName}. Waiting {Timespan} before next attempt.",
                        exception.GetType().Name, timespan);
                });

        await retryPolicy.ExecuteAsync(action);
    }
    public abstract Task Handle(INotification notification, CancellationToken cancellationToken);
}
