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

    private void ValidateRequestNotNull<T>(T request)
    {
        if (request == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Body"));
    }
    private async Task<Coupon> ValidateCoupon(string ProductName) => await _discountRepository.GetDiscount(ProductName)
           ?? throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        ValidateRequestNotNull(request);

        var coupon = await ValidateCoupon(request.ProductName);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        ValidateRequestNotNull(request);

        var coupon = await _discountRepository.GetDiscount(request.Coupon.ProductName);
        if (coupon == null)
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
        return getCouponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        ValidateRequestNotNull(request);
        await ValidateCoupon(request.Coupon.ProductName);
        var updateCoupon = _mapper.Map<CouponModel, Coupon>(request.Coupon);
        await _mediator.Publish(new DiscountUpdatedDomainEvent(updateCoupon, string.Empty));
        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        ValidateRequestNotNull(request);
        var coupon = await ValidateCoupon(request.ProductName); 
        string EventMessage = string.Empty;
        coupon.Act_Ind = 0;
        coupon.Del_Ind = 1;
        await _mediator.Publish(new DiscountDeletedDomainEvent(coupon, EventMessage));
        return new DeleteDiscountResponse() { Success = true };
    }
}
