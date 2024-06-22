namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; set; }
    private CustomerId(Guid value) => Value = value;
    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException($"{nameof(CustomerId)} cannot be empty");
        return new CustomerId(value);
    }
}
public record OrderName
{
    private const int DefaultLength = 5;
    public string? Value { get; set; }
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
        if (string.IsNullOrEmpty(value))
            throw new DomainException($"{nameof(OrderName)} cannot be empty");
        return new OrderName(value);
    }
}

public record OrderId
{
    public Guid Value { get; set; }
    private OrderId(Guid value) => Value = value;
    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException($"{nameof(OrderId)} cannot be empty");
        return new OrderId(value);
    }
}

public record ProductId
{
    public Guid Value { get; set; }
    private ProductId(Guid value) => Value = value;
    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException($"{nameof(ProductId)} cannot be empty");
        return new ProductId(value);
    }
}
public record OrderItemId
{
    public Guid Value { get; set; }
    private OrderItemId(Guid value) => Value = value;
    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException($"{nameof(OrderItemId)} cannot be empty");
        return new OrderItemId(value);
    }
}
