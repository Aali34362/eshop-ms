namespace Ordering.Domain.ValueObjects;

public static class OfClass
{
    public static T Of<T>(Guid value, Func<Guid, T> factory)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
            throw new DomainException($"{nameof(T)} cannot be empty");
        return factory(value);
    }
}

public record CustomerId
{
    public Guid Value { get; set; }
    private CustomerId(Guid value) => Value = value;
    //CustomerId customerId = OfClass.Of<CustomerId>(, guid => new CustomerId(guid));

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
    public string? Value { get; set; }
}

public record OrderId
{
    public Guid Value { get; set; }
}

public record ProductId
{
    public Guid Value { get; set; }
}
public record OrderItemId
{
    public Guid Value { get; set; }
}
