namespace Shopping.Web.Services;

public interface ICatalogService
{
    //The leading slash '/' is mandatory.
    //Refit requires that all route templates for HTTP attributes (like [Get], [Post], etc.) start with /
    [Get("/catalog-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
    Task<GetProductsResponse> GetProducts(int? pageNumber = 1, int? pageSize = 10);

    [Get("/catalog-service/products/{id}")]
    Task<GetProductsResponse> GetProduct(Guid id);

    [Get("/catalog-service/products/category/{category}")]
    Task<GetProductByCategoryResponse> GetProductsByCategory(string category);
}