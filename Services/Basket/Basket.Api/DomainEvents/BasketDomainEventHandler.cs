namespace Basket.API.DomainEvents;

internal sealed class BasketCreatedDomainEventHandler
    (ILogger<BasketCreatedDomainEventHandler> logger,
    IBasketRepository repository) 
    : BaseEventHandler<BasketCreatedDomainEvent,IBasketRepository>(logger, repository)
{

    public override async Task Handle(BasketCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string eventState = string.Empty;
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff} - Current Event State {eventState}",
                nameof(BasketCreatedDomainEventHandler), DateTime.Now, eventState);
            await Task.Delay(300);

            await _repository.StoreBasket(notification.CreatedCart, cancellationToken);
        });
    }
}

internal sealed class BasketUpdatedDomainEventHandler
    (ILogger<BasketUpdatedDomainEventHandler> logger,
    IBasketRepository repository)
    : BaseEventHandler<BasketUpdatedDomainEvent, IBasketRepository>(logger, repository)
{

    public override async Task Handle(BasketUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff}",
                nameof(BasketUpdatedDomainEventHandler), DateTime.Now);
            await Task.Delay(300);

            await _repository.UpdateBasket(notification.CreatedCart);
        });
    }
}

internal sealed class BasketDeletedDomainEventHandler
    (ILogger<BasketDeletedDomainEventHandler> logger,
    IBasketRepository repository)
    : BaseEventHandler<BasketDeletedDomainEvent, IBasketRepository>(logger, repository)
{

    public override async Task Handle(BasketDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await ExecuteWithRetryPolicy(async () =>
        {
            _logger.LogInformation("Call Handle Task - {EventName} at: {EventTime:MM/dd/yyyy HH:mm:ss.fff}",
                nameof(BasketDeletedDomainEventHandler), DateTime.Now);
            await Task.Delay(300);

            await _repository.DeleteBasket(notification.UserName, cancellationToken);
        });
    }
}
