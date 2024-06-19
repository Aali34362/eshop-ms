namespace Discount.Grpc.MappingConfigure;

public class DiscountMapper : Profile
{
    public DiscountMapper()
    {
        CreateMap<Coupon, CouponModel>();
        CreateMap<CouponModel, Coupon>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom<GuidResolver>())
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
    }
}

public class GuidResolver : IValueResolver<CouponModel, Coupon, Guid>
{
    public Guid Resolve(CouponModel source, Coupon destination, Guid destMember, ResolutionContext context)
    {
        return Guid.TryParse(source.Id, out var id) ? id : Guid.Empty;
    }
}