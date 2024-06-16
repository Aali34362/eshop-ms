namespace BaseIntegrated.BaseCQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{

}

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{

}



/*
 * Theory to understand
   Certainly! Let's break down the provided code and understand its purpose and structure. The code defines two generic interfaces, ICommandHandler and ICommandHandler<TCommand, TResponse>, which are part of a command handling mechanism, likely using the Command Pattern and adhering to certain constraints.
Interfaces Overview
1. ICommandHandler<in TCommand>

csharp

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}

    Generic Parameter TCommand: This interface takes a single generic parameter TCommand.
    Variance Annotation in: The in keyword specifies that TCommand is contravariant, meaning you can use a less derived type than originally specified.
    Inheritance: It inherits from ICommandHandler<TCommand, Unit>.
    Constraint: TCommand must implement ICommand<Unit>, meaning it must be a command that does not return any meaningful value (similar to a void return type).

2. ICommandHandler<in TCommand, TResponse>

csharp

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}

    Generic Parameters: This interface takes two generic parameters, TCommand and TResponse.
    Variance Annotation in: The in keyword specifies that TCommand is contravariant.
    Inheritance: It inherits from IRequestHandler<TCommand, TResponse>.
    Constraints:
        TCommand must implement ICommand<TResponse>.
        TResponse must be a non-nullable type (notnull).

Purpose and Usage

The two interfaces define a structure for handling commands with and without return values.
ICommandHandler<in TCommand>

    This interface is for handling commands that do not return any meaningful result (Unit).
    By inheriting from ICommandHandler<TCommand, Unit>, it leverages the more generic command handler interface but fixes the response type to Unit.

ICommandHandler<in TCommand, TResponse>

    This is a more generic interface for handling commands that return a result of type TResponse.
    The where TCommand : ICommand<TResponse> constraint ensures that TCommand is a command that returns TResponse.
    The where TResponse : notnull constraint ensures that the response type cannot be null, enhancing type safety.

Example Usage

To understand these interfaces better, let's look at how they might be implemented and used.
Command Definitions

csharp

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

public interface ICommand : ICommand<Unit>
{
}

Implementing Command Handlers

csharp

public class SampleCommand : ICommand<string>
{
    public string Data { get; set; }
}

public class SampleCommandHandler : ICommandHandler<SampleCommand, string>
{
    public Task<string> Handle(SampleCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Handled: {command.Data}");
    }
}

public class VoidCommand : ICommand
{
    public string Action { get; set; }
}

public class VoidCommandHandler : ICommandHandler<VoidCommand>
{
    public Task<Unit> Handle(VoidCommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Action performed: {command.Action}");
        return Task.FromResult(Unit.Value);
    }
}

Explanation

    SampleCommand: A command that returns a string.
    SampleCommandHandler: Implements ICommandHandler<SampleCommand, string>, providing the logic to handle SampleCommand and return a string.
    VoidCommand: A command that does not return a value.
    VoidCommandHandler: Implements ICommandHandler<VoidCommand>, providing the logic to handle VoidCommand and returns Unit.

Summary

The ICommandHandler<in TCommand> and ICommandHandler<in TCommand, TResponse> interfaces define a flexible and type-safe structure for handling commands in a C# application. By leveraging generics, covariance, and constraints, these interfaces ensure that command handlers are correctly typed and can handle a variety of commands, both with and without return values. This design pattern promotes clean, maintainable, and scalable code.
 */
