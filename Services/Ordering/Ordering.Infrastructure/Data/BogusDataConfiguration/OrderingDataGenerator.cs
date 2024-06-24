using Bogus;

namespace Ordering.Infrastructure.Data.BogusDataConfiguration;

public class OrderingDataGenerator
{
    public static Faker<Product> GetProductFaker()
    {
        return new Faker<Product>()
            .CustomInstantiator(f => Product.Create(
                ProductId.Of(f.Random.Guid()),
                f.Commerce.ProductName(),
                f.Random.Decimal(1, 1000)))
            .RuleFor(p => p.CreatedAt, f => f.Date.Past())
            .RuleFor(p => p.CreatedBy, f => f.Person.UserName)
            .RuleFor(p => p.LastModified, f => f.Date.Recent())
            .RuleFor(p => p.LastModifiedBy, f => f.Person.UserName);
    }
}
