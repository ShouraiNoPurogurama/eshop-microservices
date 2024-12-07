using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId,ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Delete order")
        .WithSummary("Delete Order");
    }
}