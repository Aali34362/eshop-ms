namespace Catalog.API.Products.CreateProduct;

public record CreateProductResult(Guid Id);

public record CreateProductCommand(
    string Name, 
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    ) : IRequest<CreateProductResult>;

internal sealed class CreateProductHandler 
    : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().
           WithMessage("Name is required").MaximumLength(50).
           WithMessage($"Name maximum length should be 50");
    }
}

///////////////////////////////////////

public record CreateProductsCommand(
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price
    ) : ICommand<CreateProductResult>;

public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
{
    public CreateProductsCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}


internal sealed class CreateProductsHandler(
    IMapper mapper, 
    IDocumentSession session
    ////,IValidator<CreateProductsCommand> validator
    )
    : ICommandHandler<CreateProductsCommand, CreateProductResult>
{
    private readonly IMapper _mapper = mapper;

    public async Task<CreateProductResult> Handle(CreateProductsCommand command, CancellationToken cancellationToken)
    {
        //Create Product entity from command object 
        //save to database
        //return CreateProductResult result
        
        //Adding Manual Validation through Validator
        ////var result = await validator.ValidateAsync(command, cancellationToken);
        ////var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        ////if (errors.Count == 0)
        ////{
        ////    throw new ValidationException(System.Text.Json.JsonSerializer.Serialize(errors));
        ////}
        
            Product product = _mapper.Map<CreateProductsCommand, Product>(command);
            product.Lst_Crtd_Date = DateTime.Now;

            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateProductResult(product.Id);
    }
}