namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<string> CreateBasket(ShoppingCart shoppingCart);
    Task<string> UpdateBasket(ShoppingCart shoppingCart);
    Task<string> DeleteBasket(string UserName);
}
