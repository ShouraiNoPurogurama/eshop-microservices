namespace Shopping.Web.Pages;
public class IndexModel
    (ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger)
    : PageModel
{    
    public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();    

    public async Task<IActionResult> OnGetAsync() //This page will be triggered when user open Index page
    {
        logger.LogInformation("Index page visited.");
        var result = await catalogService.GetProducts();
        ProductList = result.Products;
        return Page();
    }
}
