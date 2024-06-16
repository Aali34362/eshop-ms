namespace Catalog.API.Products.GetProductByCategory;


////public record GetProductByCategoryRequest();
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", 
            async (string category, ISender sender) =>
        {
            var request = await sender.Send(new GetProductByCategoryQuery(category));
            var response = request.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);//commented because Map function is not working
            ////return Results.Ok(request);
        }).WithName("GetProductCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
