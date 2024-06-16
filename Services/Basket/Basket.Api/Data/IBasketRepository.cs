namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default);
    Task<bool> UpdateBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default);
    Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default);
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default);
}
