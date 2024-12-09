using Shopping.Web.Models.Ordering;

namespace Shopping.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
    public Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);

    [Get("/ordering-service/orders?name={name}")]
    public Task<GetOrdersByNameResponse> GetOrdersByName(string name);

    [Get("/ordering-service/orders/customer/{customerId}")]
    public Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
}