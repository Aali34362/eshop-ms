namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get; private set; } = default!;
    public string CardNumber { get; private set; } = default!;
    public string Expiration { get; private set; } = default!;
    public string CW { get; private set; } = default!;
    public int PaymentMethod { get; private set; } = default!;
    protected Payment() {}
    private Payment(string CardName, string CardNumber, string Expiration, string CW, int PaymentMethod)
    {
        this.CardName = CardName;
        this.CardNumber = CardNumber;
        this.Expiration = Expiration;
        this.CW = CW;
        this.PaymentMethod = PaymentMethod;
    }
    public static Payment Of(string CardName, string CardNumber, string Expiration, string CW, int PaymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(CardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(CW);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(CW.Length,3);
        return new Payment(CardName, CardNumber, Expiration, CW, PaymentMethod);
    }
}
