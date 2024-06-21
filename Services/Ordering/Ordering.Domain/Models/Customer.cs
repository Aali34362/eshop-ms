namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    /*So create Method is factor method that encapsulate the creation of Customer instance in this
     create method, and it includes domain validations to ensure the integrity of the customer instance.
     These are the domain validations and after that we have create a new customer using the default constructor
     and return the customer in the create method.
     So basically we have created static create method that create customer in the customer entity, but

customer entity using strongly typed IDs which is the customer ID value objects.

So these objects also need to create its own type.

For that purpose we will create off method.

So that means in order to separate entities and value objects for the create responsibilities, we will

develop the static create method for each entities under the model folder.

And we will also develop the static off method for each value objects under the value object folder.

For that purpose, please go to the customer ID value object and this is the value object.

And this value object also responsible to create its own type with accepting the Guid value.

So we will develop a static off method in order to create instance of customer id for that purpose.  

     
     */
    public static Customer Create(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        var customer = new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };
        return customer;
    }
}
