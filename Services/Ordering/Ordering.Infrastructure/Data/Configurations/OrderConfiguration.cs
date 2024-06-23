

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(oi => oi.CustomerId);

        builder.HasMany<OrderItem>()
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(n => n.FirstName)
                .HasColumnName(nameof(Address.FirstName))
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(n => n.LastName)
                .HasColumnName(nameof(Address.LastName))
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(n => n.EmailAddress)
                .HasColumnName(nameof(Address.EmailAddress))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.EmailAddress)
                .HasColumnName(nameof(Address.EmailAddress))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.AddressLine)
                .HasColumnName(nameof(Address.AddressLine))
                .HasMaxLength(100).IsRequired();

                addressBuilder.Property(n => n.Country)
                .HasColumnName(nameof(Address.Country))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.State)
                .HasColumnName(nameof(Address.State))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.ZipCode)
                .HasColumnName(nameof(Address.ZipCode))
                .HasMaxLength(5);
            });

        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(n => n.FirstName)
                .HasColumnName(nameof(Address.FirstName))
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(n => n.LastName)
                .HasColumnName(nameof(Address.LastName))
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(n => n.EmailAddress)
                .HasColumnName(nameof(Address.EmailAddress))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.EmailAddress)
                .HasColumnName(nameof(Address.EmailAddress))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.AddressLine)
                .HasColumnName(nameof(Address.AddressLine))
                .HasMaxLength(100).IsRequired();

                addressBuilder.Property(n => n.Country)
                .HasColumnName(nameof(Address.Country))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.State)
                .HasColumnName(nameof(Address.State))
                .HasMaxLength(50);

                addressBuilder.Property(n => n.ZipCode)
                .HasColumnName(nameof(Address.ZipCode))
                .HasMaxLength(5);
            });

        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(n => n.CardName)
                .HasColumnName(nameof(Payment.CardName))
                .HasMaxLength(50);

                paymentBuilder.Property(n => n.CardNumber)
               .HasColumnName(nameof(Payment.CardNumber))
               .HasMaxLength(24)
               .IsRequired();

                paymentBuilder.Property(n => n.CardNumber)
              .HasColumnName(nameof(Payment.CardNumber))
              .HasMaxLength(24)
              .IsRequired();

                paymentBuilder.Property(n => n.Expiration)
              .HasColumnName(nameof(Payment.Expiration))
              .HasMaxLength(10);

                paymentBuilder.Property(n => n.CVV)
              .HasColumnName(nameof(Payment.CVV))
              .HasMaxLength(3);
            });

        builder.Property(c => c.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
            s => s.ToString(),
            dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
