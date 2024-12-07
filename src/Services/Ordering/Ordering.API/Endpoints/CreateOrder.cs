using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

// - Accepts the createOrderDto as a parameter
// - Construct a CreateOrderCommand
// - Sends the command using mediatR
// - Returns a success or not bad request response

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.Id}", response);
            })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Create order")
            .WithSummary("Create Order");
    }
}