namespace Shopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalogService, IBasketService basketService, ILogger<ProductListModel> logger)
        : PageModel
    {
        public IEnumerable<string> CategoryList { get; set; } = [];
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        [BindProperty(SupportsGet = true)] //Means set the SelectedCategory when the application opens the page
        public string SelectedCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var response = await catalogService.GetProducts();

            CategoryList = response.Products.SelectMany(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = response.Products.Where(p => p.Category.Contains(categoryName));
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = response.Products;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            var product = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();
            
            basket.Items.Add(new ShoppingCartItemModel()
            {
                ProductId = productId,
                ProductName = product.Product.Name,
                Price = product.Product.Price,
                Quantity = 1,
                Color = "Black"
            });
            
            return RedirectToPage("Cart");
        }
    }
}