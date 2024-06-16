namespace Catalog.API.Configurations;

public class CatalogMapper : Profile
{
    public CatalogMapper()
    {
        CreateMap<CreateProductsCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
