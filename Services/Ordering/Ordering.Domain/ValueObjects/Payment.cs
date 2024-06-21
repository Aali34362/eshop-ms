namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; private set; } = default!;
    public string CardNumber { get; private set; } = default!;
    public string Expiration { get; private set; } = default!;
    public string CW { get; private set; } = default!;
    public string Payment { get; private set; } = default!;
}
