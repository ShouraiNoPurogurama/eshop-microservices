namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);
    
    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);
    
    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName);
    
    async Task<ShoppingCartModel> LoadUserBasket()
    {
        //Get Basket, if not exist => Create new Basket with Default logged-in userName
        var userName = "swn";
        ShoppingCartModel basket;

        try
        {
            var getBasketResponse = await GetBasket(userName); //able to call interface's method
            basket = getBasketResponse.Cart;
        }
        catch (Exception e)
        {
            basket = new ShoppingCartModel
            {
                UserName = userName,
                Items = []
            };
        }

        return basket;
    }
}