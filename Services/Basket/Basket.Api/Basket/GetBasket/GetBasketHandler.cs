namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository basketRepository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public IBasketRepository _basketRepository = basketRepository ?? throw new Exception();
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken) =>
        new GetBasketResult(await _basketRepository.GetBasket(request.UserName, cancellationToken));
}
