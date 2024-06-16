namespace Base.BaseCQRS;

public interface ICommand : ICommand<Unit>{}
public interface ICommand<out TResponse> : IRequest<TResponse>{}

/*
    Theory for understanding behind use of concept

    Certainly! Let's revisit the ICommand interfaces in your C# code to understand their purpose and how they fit into a typical Command Pattern.
Understanding the Code
ICommand Interface (Non-generic)

csharp

public interface ICommand : ICommand<Unit>
{
}

    Purpose: This is a non-generic interface for commands that do not return any meaningful result. By inheriting from ICommand<Unit>, it simplifies the creation of such commands by avoiding the need to specify Unit explicitly.
    Unit Type: In C#, Unit is often used to represent a void return type, borrowed from functional programming languages. It signifies that the command does not return any value.

ICommand<TResponse> Interface (Generic)

csharp

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

    Generic Parameter TResponse: This interface is generic and allows specifying the type of the response (TResponse) the command returns.
    Covariance (out keyword): The out keyword indicates that TResponse is covariant, meaning you can use a more derived type than originally specified. This is useful for flexibility in handling command results.
    Inheritance from IRequest<TResponse>: By inheriting from IRequest<TResponse>, this interface can be used in systems (like MediatR) that handle requests and responses, integrating commands into such frameworks.

Example Scenario

To illustrate how these interfaces might be used, let's create some example commands and their handlers.
Example Commands

csharp

// Command that does not return a value
public class CreateOrderCommand : ICommand
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
}

// Command that returns a result
public class GetOrderStatusCommand : ICommand<string>
{
    public int OrderId { get; set; }
}

    CreateOrderCommand: Implements ICommand, meaning it does not return any value upon execution.
    GetOrderStatusCommand: Implements ICommand<string>, meaning it returns a string result, such as the status of an order.

Example Command Handlers

csharp

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    public Task<Unit> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Logic to create an order
        Console.WriteLine($"Order created: {command.OrderId} for product {command.ProductName}");
        return Task.FromResult(Unit.Value);
    }
}

public class GetOrderStatusCommandHandler : ICommandHandler<GetOrderStatusCommand, string>
{
    public Task<string> Handle(GetOrderStatusCommand command, CancellationToken cancellationToken)
    {
        // Logic to get the order status
        string status = "Order shipped"; // Example status
        return Task.FromResult(status);
    }
}

    CreateOrderCommandHandler: Handles CreateOrderCommand and returns Unit, indicating the command completes without returning a value.
    GetOrderStatusCommandHandler: Handles GetOrderStatusCommand and returns a string, providing the status of the order.

Putting It All Together

These interfaces and handlers can be integrated into a command processing system, typically using a mediator pattern (like MediatR), to decouple the request (command) from its handling logic.
Example Usage with MediatR

csharp

public class Program
{
    private readonly IMediator _mediator;

    public Program(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RunAsync()
    {
        // Create an order
        var createOrderCommand = new CreateOrderCommand { OrderId = 1, ProductName = "Book" };
        await _mediator.Send(createOrderCommand);

        // Get the order status
        var getOrderStatusCommand = new GetOrderStatusCommand { OrderId = 1 };
        string status = await _mediator.Send(getOrderStatusCommand);

        Console.WriteLine($"Order Status: {status}");
    }
}

Summary

    ICommand: A non-generic interface for commands that do not return a value, inheriting from ICommand<Unit>.
    ICommand<TResponse>: A generic interface for commands that return a result of type TResponse, inheriting from IRequest<TResponse>.
    Use Cases: These interfaces allow defining commands with and without return values, facilitating a clean implementation of the Command Pattern. They can be seamlessly integrated into a mediator framework like MediatR for processing commands and handling responses in a decoupled manner.
 */