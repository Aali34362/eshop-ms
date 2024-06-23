namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public string AddressLine { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;
    protected Address() { }
    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country,string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        //ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        //ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        //ArgumentException.ThrowIfNullOrWhiteSpace(country);
        //ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
    }
}
