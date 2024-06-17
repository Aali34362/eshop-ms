namespace Discount.Grpc.Models;

public class Coupon : BaseRequest
{
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; } = 0;
}
