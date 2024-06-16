namespace Catalog.API.Products.CreateProduct;

public record CreateProductsRequest(
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    );

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async(
            CreateProductsRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductsCommand>();//Using Mapster
            //Why we need command object because our mediator is requiring command object in order to trigger our command handler.
            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
