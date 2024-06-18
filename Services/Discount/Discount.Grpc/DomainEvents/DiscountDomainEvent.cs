namespace Discount.Grpc.DomainEvents;

public class DiscountCreatedDomainEvent(Coupon createdCoupon, string Message) : BaseDomainEvent(Message)
{
    public Coupon CreatedCoupon = createdCoupon;
}

public class DiscountUpdatedDomainEvent(Coupon updatedCoupon, string Message) : BaseDomainEvent(Message)
{
    public Coupon UpdatedCoupon = updatedCoupon;
}

public class DiscountDeletedDomainEvent(string productName, string Message) : BaseDomainEvent(Message)
{
    public readonly string ProductName = productName;
}
