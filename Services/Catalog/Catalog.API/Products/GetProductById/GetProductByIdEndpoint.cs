
namespace Catalog.API.Products.GetProductById;

////public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var request = await sender.Send(new GetProductByIdQuery(id));
            var response = request.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);//commented because Map function is not working
            ////return Results.Ok(request);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
