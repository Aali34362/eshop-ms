using Bogus;

namespace Ordering.Infrastructure.Data.BogusDataConfiguration;

public class OrderingDataGenerator(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
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

    public static Faker<Customer> GetCustomerFaker()
    {
        return new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(
                CustomerId.Of(f.Random.Guid()),
                f.Person.FullName,
                f.Internet.Email()))
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.CreatedBy, f => f.Person.UserName)
            .RuleFor(c => c.LastModified, f => f.Date.Recent())
            .RuleFor(c => c.LastModifiedBy, f => f.Person.UserName);
    }

    private List<Product> GetProducts()
    {
        return _applicationDbContext.Products.ToList();
    }

    private List<Customer> GetCustomer()
    {
        return _applicationDbContext.Customers.ToList();
    }

    #region Orders
    public static Faker<Address> GetAddressFaker()
    {
        return new Faker<Address>()
           .CustomInstantiator(f => Address.Of(
               f.Name.FirstName(),
               f.Name.LastName(),
               f.Internet.Email(),
               f.Address.StreetAddress(),
               f.Address.Country(),
               f.Address.State(),
               f.Address.ZipCode()));
    }

    public static Faker<Payment> GetPaymentFaker()
    {
        return new Faker<Payment>()
            .CustomInstantiator(f => Payment.Of(
                f.Name.FullName(),
                f.Finance.CreditCardNumber(),
                f.Date.Future().ToString("MM/yy"),
                f.Finance.CreditCardCvv(),
                f.PickRandom<int>(1, 2, 3, 4, 5)));
    }

    public static Faker<OrderItem> GetOrderItemFaker()
    {
        return new Faker<OrderItem>()
            .CustomInstantiator(f => new OrderItem(
                OrderId.Of(f.Random.Guid()),
                ProductId.Of(f.Random.Guid()),
                f.Random.Int(1, 10),
                f.Random.Decimal(1, 1000)));
    }

    public static Faker<Order> GetOrderFaker()
    {
        var billingAddressFaker = GetAddressFaker();
        var shippingAddressFaker = GetAddressFaker();
        var paymentFaker = GetPaymentFaker();
        var orderItemFaker = GetOrderItemFaker();

        return new Faker<Order>()
            .CustomInstantiator(f => Order.Create(
                OrderId.Of(f.Random.Guid()),
                CustomerId.Of(f.Random.Guid()),
                OrderName.Of(f.Commerce.ProductName()),
                shippingAddressFaker.Generate(),
                billingAddressFaker.Generate(),
                paymentFaker.Generate()))
            .RuleFor(o => o.OrderItems, f => orderItemFaker.Generate(f.Random.Int(1, 5)).ToList())
            .RuleFor(o => o.CreatedAt, f => f.Date.Past())
            .RuleFor(o => o.CreatedBy, f => f.Person.UserName)
            .RuleFor(o => o.LastModified, f => f.Date.Recent())
            .RuleFor(o => o.LastModifiedBy, f => f.Person.UserName);
    }
    #endregion
}
