namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    ) 
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}

internal sealed class UpdateProductCommandHandler
    (IMapper mapper, IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IMapper _mapper = mapper;

    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command, 
        CancellationToken cancellationToken)
    {
        var updateProduct = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);

        Product product = _mapper.Map<UpdateProductCommand, Product>(command);
        product.Crtd_Date = updateProduct.Crtd_Date;
        product.Crtd_User = updateProduct.Crtd_User;
        product.Lst_Crtd_Date = DateTime.Now;
        product.Lst_Crtd_User = "XYZ";
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}