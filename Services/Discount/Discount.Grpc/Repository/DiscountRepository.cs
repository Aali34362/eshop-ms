namespace Discount.Grpc.Repository;

public class DiscountRepository(DiscountContext discountContext, ILogger<DiscountRepository> logger) : IDiscountRepository
{
    private readonly DiscountContext _discountContext = discountContext ?? throw new ArgumentNullException(nameof(discountContext));
    private readonly ILogger<DiscountRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        ////abc
    }

    public async Task<Coupon> GetDiscount(string productName, CancellationToken cancellation = default)=>
        await _discountContext.Coupons.FirstOrDefaultAsync(x => x.ProductName.Equals(productName));
    

    public async Task<bool> CreateDiscount(Coupon request, CancellationToken cancellation = default)
    {
        using var transaction = await _discountContext.Database.BeginTransactionAsync();
        try
        {
            await _discountContext.AddAsync<Coupon>(request);
            await _discountContext.SaveChangesAsync();
            await transaction.CommitAsync(cancellation);
        }
        catch(Exception)
        {
            await transaction.RollbackAsync(cancellation);
            throw;
        }
        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon request, CancellationToken cancellation = default)
    {
        using var transaction = await _discountContext.Database.BeginTransactionAsync();
        try
        {
            _discountContext.Update<Coupon>(request);
            await _discountContext.SaveChangesAsync();
            await transaction.CommitAsync(cancellation);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellation);
            throw;
        }
        return true;
    }

    public async Task<bool> DeleteDiscount(Coupon request, CancellationToken cancellation = default)
    {
        using var transaction = await _discountContext.Database.BeginTransactionAsync();
        try
        {
            _discountContext.Update<Coupon>(request);
            await _discountContext.SaveChangesAsync();
            await transaction.CommitAsync(cancellation);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellation);
            throw;
        }
        return true;
    }
}
