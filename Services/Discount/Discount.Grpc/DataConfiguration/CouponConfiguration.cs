using static Grpc.Core.Metadata;

namespace Discount.Grpc.DataConfiguration;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(x => x.Id).HasName("pk_coupon");
        builder.Property(e => e.ProductName)
                .HasMaxLength(250)
                .IsUnicode(false);
        builder.Property(e => e.Description)
                .HasMaxLength(int.MaxValue)
                .IsUnicode(false);
    }
}
