namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository _basketRepository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default)
    {
        await _basketRepository.DeleteBasket(UserName, cancellation);
        await cache.RemoveAsync(UserName,cancellation);
        return true;
    }
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellation);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        var basket = await _basketRepository.GetBasket(userName, cancellation);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellation);
        return basket;
    }
    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default)
    {
        await _basketRepository.StoreBasket(shoppingCart, cancellation);
        await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellation);
        return shoppingCart;
    }
    public async Task<bool> UpdateBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default) =>
       await _basketRepository.UpdateBasket(shoppingCart, cancellation);
}
