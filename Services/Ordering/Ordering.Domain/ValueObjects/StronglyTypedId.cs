namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value {  get; set; }
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
