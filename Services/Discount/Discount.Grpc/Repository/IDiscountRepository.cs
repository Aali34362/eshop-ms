namespace Discount.Grpc.Repository;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName, CancellationToken cancellation = default);
    Task<bool> CreateDiscount(Coupon request, CancellationToken cancellation = default);
    Task<bool> UpdateDiscount(Coupon request, CancellationToken cancellation = default);
    Task<bool> DeleteDiscount(string productName, CancellationToken cancellation = default);
}
