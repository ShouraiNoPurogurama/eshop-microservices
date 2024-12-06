using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.Order.Id).NotEmpty().WithMessage("Order Id is required.");
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Order Name cannot be empty.");
        RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("Customer Id is required.");
    }
}