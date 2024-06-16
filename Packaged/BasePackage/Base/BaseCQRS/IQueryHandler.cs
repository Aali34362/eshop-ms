namespace Base.BaseCQRS;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}

/*
 * Understanding the Code
   Components of the Interface

    Generic Parameters:
        TQuery: The type of the query that this handler will handle.
        TResponse: The type of the response that the query returns.

    Variance Annotation (in):
        The in keyword specifies that TQuery is contravariant. Contravariance allows you to use a less derived type than originally specified. This is useful for flexibility in handling different types of queries.

    Inheritance from IRequestHandler<TQuery, TResponse>:
        This interface inherits from IRequestHandler<TQuery, TResponse>, which is a part of libraries like MediatR used to handle requests and responses. This means that IQueryHandler must implement the Handle method defined by IRequestHandler.

    Constraints:
        where TQuery : IQuery<TResponse>: Ensures that TQuery implements the IQuery<TResponse> interface, making it a valid query type that returns TResponse.
        where TResponse : notnull: Ensures that TResponse cannot be null, enhancing type safety.

Purpose and Usage

The IQueryHandler<in TQuery, TResponse> interface is designed to handle queries in the context of the Command Query Responsibility Segregation (CQRS) pattern. In CQRS, commands and queries are handled separately: commands modify state, while queries retrieve state.
Example Scenario

To illustrate how this interface might be used, let's create example queries and their handlers.
Example Queries

csharp

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

public class GetOrderByIdQuery : IQuery<Order>
{
    public int OrderId { get; set; }

    public GetOrderByIdQuery(int orderId)
    {
        OrderId = orderId;
    }
}

public class GetAllOrdersQuery : IQuery<IEnumerable<Order>>
{
}

    GetOrderByIdQuery: A query to get a specific order by its ID, returning an Order object.
    GetAllOrdersQuery: A query to get all orders, returning a collection of Order objects.

Example Query Handlers

csharp

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = _orderRepository.GetById(query.OrderId);
        return Task.FromResult(order);
    }
}

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<IEnumerable<Order>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = _orderRepository.GetAll();
        return Task.FromResult(orders);
    }
}

    GetOrderByIdQueryHandler: Handles GetOrderByIdQuery and retrieves the order by its ID from the repository.
    GetAllOrdersQueryHandler: Handles GetAllOrdersQuery and retrieves all orders from the repository.

Putting It All Together

These query interfaces and handlers can be integrated into a query processing system, typically using a mediator pattern (like MediatR), to decouple the request (query) from its handling logic.
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
        // Get a specific order by ID
        var getOrderByIdQuery = new GetOrderByIdQuery(1);
        Order order = await _mediator.Send(getOrderByIdQuery);
        Console.WriteLine($"Order ID: {order.Id}, Product: {order.ProductName}");

        // Get all orders
        var getAllOrdersQuery = new GetAllOrdersQuery();
        IEnumerable<Order> orders = await _mediator.Send(getAllOrdersQuery);
        foreach (var ord in orders)
        {
            Console.WriteLine($"Order ID: {ord.Id}, Product: {ord.ProductName}");
        }
    }
}

Summary

    IQueryHandler<in TQuery, TResponse>: A generic interface for handling queries that inherit from IRequestHandler<TQuery, TResponse>.
    Contravariance: Allows the use of a less derived type for TQuery, providing flexibility in handling different types of queries.
    Constraints: Ensures that TQuery implements IQuery<TResponse> and TResponse is a non-nullable type, enhancing type safety.
    Usage in CQRS: Separates queries (for retrieving state) from commands (for modifying state), typically used with a mediator pattern to decouple request handling logic.

This design promotes clean, maintainable, and scalable code by clearly separating the responsibilities of commands and queries and ensuring type safety through constraints and variance.
 */
