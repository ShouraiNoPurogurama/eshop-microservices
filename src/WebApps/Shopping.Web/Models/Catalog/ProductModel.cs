namespace Shopping.Web.Models.Catalog;

public class ProductModel //Product Entity
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = [];
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}

//Wrapper classes
public record GetProductsResponse(IEnumerable<ProductModel> Products)
{
    public ProductModel Product { get; set; }
}

public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);

public record GetProductByIdResponse(ProductModel Product);