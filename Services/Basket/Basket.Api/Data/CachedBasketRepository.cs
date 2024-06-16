namespace Basket.API.Data;

public class CachedBasketRepository : IBasketRepository
{
    public Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
