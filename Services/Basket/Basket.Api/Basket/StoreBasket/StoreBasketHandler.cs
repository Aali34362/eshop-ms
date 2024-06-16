using Basket.API.DomainEvents;
using Marten.Events;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
////public record StoreBasketResult(bool IsSuccess);
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("User Name required");
    }
}

public class StoreBasketCommandHandler(IMediator mediator)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;
        try
        {
            string EventMessage = string.Empty;
            var handler = _mediator.Publish(new BasketCreatedDomainEvent(cart, EventMessage), cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {

        }
        return new StoreBasketResult("Abc");
    }

    protected virtual void OnSuccess<TEvent>(TEvent @event) where TEvent : IEvent
    {
        // Implement success logic, e.g., logging
        Console.WriteLine($"Event {@event.GetType().Name} handled successfully.");
    }

    protected virtual void OnException<TEvent>(TEvent @event, Exception ex) where TEvent : IEvent
    {
        // Implement exception logic, e.g., logging
        Console.WriteLine($"Event {@event.GetType().Name} handling failed with exception: {ex.Message}");
    }
}
