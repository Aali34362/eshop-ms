using Ordering.Infrastructure.Data.BogusDataConfiguration;

namespace Ordering.Infrastructure.Data.Extensions;

public class InitialData
{
    ////var productFaker = DataGenerator.GetProductFaker();
    ////List<Product> fakeProducts = productFaker.Generate(100);
    public static IEnumerable<Customer> Customers => new[]
    { 
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"ABC","abc@gmail.com"),
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"XYZ","xyz@gmail.com")
    };

    public static IEnumerable<Product> Products => 
        OrderingDataGenerator.GetProductFaker().Generate(10);
}
