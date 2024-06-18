namespace Discount.Grpc.DomainEvents;

public class DiscountCreatedDomainEvent(Coupon createdCoupon, string Message) : BaseDomainEvent(Message)
{
    public Coupon CreatedCoupon = createdCoupon;
}

public class DiscountUpdatedDomainEvent(Coupon updatedCoupon, string Message) : BaseDomainEvent(Message)
{
    public Coupon UpdatedCoupon = updatedCoupon;
}

public class DiscountDeletedDomainEvent(Coupon deletedCoupon, string Message) : BaseDomainEvent(Message)
{
    public readonly Coupon DeletedCoupon = deletedCoupon;
}
