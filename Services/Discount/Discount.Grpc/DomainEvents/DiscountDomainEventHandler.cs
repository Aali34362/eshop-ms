namespace Discount.Grpc.DomainEvents;

internal sealed class DiscountCreatedDomainEventHandler
    (ILogger<DiscountCreatedDomainEventHandler> logger,
    IDiscountRepository repository)
    : BaseEventHandler<DiscountCreatedDomainEvent, IDiscountRepository>(logger, repository)
{

    public override async Task Handle(DiscountCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string eventState = string.Empty;
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff} - Current Event State {eventState}",
                nameof(DiscountCreatedDomainEventHandler), DateTime.Now, eventState);
            await Task.Delay(300);

            await _repository.CreateDiscount(notification.CreatedCoupon, cancellationToken);
        });
    }
}

internal sealed class DiscountUpdatedDomainEventHandler
    (ILogger<DiscountUpdatedDomainEventHandler> logger,
    IDiscountRepository repository)
    : BaseEventHandler<DiscountUpdatedDomainEvent, IDiscountRepository>(logger, repository)
{

    public override async Task Handle(DiscountUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff}",
                nameof(DiscountUpdatedDomainEventHandler), DateTime.Now);
            await Task.Delay(300);

            await _repository.UpdateDiscount(notification.UpdatedCoupon);
        });
    }
}

internal sealed class DiscountDeletedDomainEventHandler
    (ILogger<DiscountDeletedDomainEventHandler> logger,
    IDiscountRepository repository)
    : BaseEventHandler<DiscountDeletedDomainEvent, IDiscountRepository>(logger, repository)
{

    public override async Task Handle(DiscountDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff}",
                nameof(DiscountDeletedDomainEventHandler), DateTime.Now);
            await Task.Delay(300);

            await _repository.DeleteDiscount(notification.ProductName, cancellationToken);
        });
    }
}
