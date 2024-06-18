namespace Discount.Grpc.Repository;

public class DiscountRepository : IDiscountRepository
{
    public Task<Coupon> GetDiscount(string productName, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateDiscount(Coupon request, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDiscount(Coupon request, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDiscount(string productName, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }
}
