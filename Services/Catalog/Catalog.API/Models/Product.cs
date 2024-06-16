namespace Catalog.API.Models;

public class Product : BaseRequest
{    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Category { get; set; } = [];
    public string ImageFile { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
}
