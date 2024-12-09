namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItemModel> Items { get; set; } = [];
    
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

//Wrapper classes
public record GetBasketResponse(ShoppingCartModel Cart);

public record StoreBasketRequest(ShoppingCartModel Cart);

public record StoreBasketResponse(string UserName);

public record DeleteBasketResponse(bool IsSuccess);