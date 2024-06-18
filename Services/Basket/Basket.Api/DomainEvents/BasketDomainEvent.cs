namespace Basket.API.DomainEvents;

public class BasketCreatedDomainEvent(ShoppingCart createdCart, string Message) : BaseDomainEvent(Message)
{
    public ShoppingCart CreatedCart = createdCart;
}   

public class BasketUpdatedDomainEvent(ShoppingCart updatedCart, string Message) : BaseDomainEvent(Message)
{
    public ShoppingCart UpdatedCart = updatedCart;
}

public class BasketDeletedDomainEvent(string userName, string Message) : BaseDomainEvent(Message)
{
    public readonly string UserName = userName;
}