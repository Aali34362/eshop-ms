namespace Discount.Grpc.Services;

public class DiscountService(IMediator mediator,IDiscountRepository discountRepository, IMapper mapper) : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IDiscountRepository _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        ////abc
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);
        var couponModel = coupon.Adapt<CouponModel>();
        couponModel.Id = coupon.Id.ToString();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.Coupon.ProductName);
        if (coupon.ProductName.Equals("Product Not Found"))
        {
            var createCoupon = _mapper.Map<CouponModel,Coupon>(request.Coupon);
            await _mediator.Publish(new DiscountCreatedDomainEvent(createCoupon, string.Empty));
        }
        else
        {
            return coupon.Adapt<CouponModel>();
        }
        
        var getCoupon = await _discountRepository.GetDiscount(request.Coupon.ProductName);
        var getCouponModel = getCoupon.Adapt<CouponModel>();
        getCouponModel.Id = getCoupon.Id.ToString();
        return getCouponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.Coupon.ProductName);
        if (coupon.ProductName.Equals("Product Not Found"))
            return coupon.Adapt<CouponModel>();

        await _mediator.Publish(new DiscountUpdatedDomainEvent(request.Coupon.Adapt<Coupon>(), string.Empty));

        request.Coupon.Id = coupon.Id.ToString();
        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);
        if(coupon.ProductName.Equals("Product Not Found"))
            return new DeleteDiscountResponse() { Success = false };

        string EventMessage = string.Empty;
        coupon.Act_Ind = 0;
        coupon.Del_Ind = 1;
        await _mediator.Publish(new DiscountDeletedDomainEvent(coupon, EventMessage));
        return new DeleteDiscountResponse() { Success = true };
    }
}
