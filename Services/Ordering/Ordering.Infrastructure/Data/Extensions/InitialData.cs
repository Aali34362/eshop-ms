using Ordering.Infrastructure.Data.BogusDataConfiguration;

namespace Ordering.Infrastructure.Data.Extensions;

public class InitialData
{
    public static IEnumerable<Customer> Customers => new[]
    { 
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"ABC","abc@gmail.com"),
        Customer.Create(CustomerId.Of(Guid.NewGuid()),"XYZ","xyz@gmail.com")
    };

    public static IEnumerable<Product> Products => 
        OrderingDataGenerator.GetProductFaker().Generate(10);

    public static IEnumerable<Order> Orders =>
       OrderingDataGenerator.GetOrderFaker().Generate(1);
}
