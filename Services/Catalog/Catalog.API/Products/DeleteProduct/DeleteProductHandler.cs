
using Marten;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var updateProduct = await session.LoadAsync<Product>(request.Id) ?? throw new ProductNotFoundException(request.Id);
        ////session.Delete<Product>(product.Id);
        updateProduct.Act_Ind = 0;
        updateProduct.Del_Ind = 1;
        session.Update(updateProduct);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
