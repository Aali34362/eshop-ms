namespace Discount.Grpc.MappingConfigure;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CouponModel, Coupon>.NewConfig()
            .Map(dest => dest.Id, src => Guid.Parse(src.Id))
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Amount, src => src.Amount);

        TypeAdapterConfig<Coupon, CouponModel>.NewConfig()
           .Map(dest => dest.Id, src => src.Id.ToString())
           .Map(dest => dest.ProductName, src => src.ProductName)
           .Map(dest => dest.Description, src => src.Description)
           .Map(dest => dest.Amount, src => src.Amount);
    }
}
