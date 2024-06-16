using Catalog.API.Models;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal sealed class GetProductByCategoryQueryHandler
(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync();
        return products is null ? throw new ProductNotFoundException(query.Category) : new GetProductByCategoryResult(products);
    }
}
