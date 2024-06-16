namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellation = default)
    {
        session.Delete<ShoppingCart>(UserName);
        await session.SaveChangesAsync();
        return true;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation = default) =>    
         await session.LoadAsync<ShoppingCart>(userName, cancellation) 
            ?? throw new BasketNotFoundException(userName);
    

    public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default)
    {
        session.Store<ShoppingCart>(shoppingCart);
        await session.SaveChangesAsync();
        return shoppingCart;
    }

    public async Task<bool> UpdateBasket(ShoppingCart shoppingCart, CancellationToken cancellation = default)
    {
        session.Update<ShoppingCart>(shoppingCart);
        await session.SaveChangesAsync();
        return true;
    }
}
