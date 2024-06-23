namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; private set; } = default!;
    public string CardNumber { get; private set; } = default!;
    public string Expiration { get; private set; } = default!;
    public string CVV { get; private set; } = default!;
    public int PaymentMethod { get; private set; } = default!;
    protected Payment() {}
    private Payment(string CardName, string CardNumber, string Expiration, string CVV, int PaymentMethod)
    {
        this.CardName = CardName;
        this.CardNumber = CardNumber;
        this.Expiration = Expiration;
        this.CVV = CVV;
        this.PaymentMethod = PaymentMethod;
    }
    public static Payment Of(string CardName, string CardNumber, string Expiration, string CVV, int PaymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(CardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(CVV);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(CVV.Length,3);
        return new Payment(CardName, CardNumber, Expiration, CVV, PaymentMethod);
    }
}
