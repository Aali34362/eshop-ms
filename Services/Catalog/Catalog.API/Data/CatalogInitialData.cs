using Bogus;
using JasperFx.CodeGeneration.Frames;
using System.Collections.Generic;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
            return;
        session.Store<Product>(GetPreconfiguredProducts());
        session.Store<Product>(GetPreconfiguredBogusProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    { 
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "IPhone X",
            Description = "This phone is ......",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = new List<string>{"Smart Dumb Phone"}
        }
    };

    private static IEnumerable<Product> GetPreconfiguredBogusProducts()
    {
       var productFaker = DataGenerator.GetProductFaker();
        List<Product> fakeProducts = productFaker.Generate(100);
        return fakeProducts;
    }
}


public class DataGenerator
{
    public static Faker<Product> GetProductFaker()
    {
        return new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.ImageFile, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Category, f => f.Make(3, () => f.Commerce.Categories(1)[0]));
    }
}
