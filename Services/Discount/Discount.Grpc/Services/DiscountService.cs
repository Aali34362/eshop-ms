namespace Discount.Grpc.Services;

public class DiscountService(IMediator mediator,IDiscountRepository discountRepository) : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IDiscountRepository _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
    public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        _discountRepository.GetDiscount(request.ProductName);
        return base.GetDiscount(request, context);
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        string EventMessage = string.Empty;
        _mediator.Publish(new DiscountCreatedDomainEvent(new Coupon(),EventMessage));
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        string EventMessage = string.Empty;
        _mediator.Publish(new DiscountUpdatedDomainEvent(new Coupon(), EventMessage));
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        string EventMessage = string.Empty;
        _mediator.Publish(new DiscountDeletedDomainEvent(request.ProductName, EventMessage));
        return base.DeleteDiscount(request, context);
    }
}
