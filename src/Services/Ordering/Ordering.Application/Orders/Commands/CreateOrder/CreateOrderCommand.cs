using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required.");
        RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("Customer ID is required.");
        RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("Order Items should not be empty.");
    }
}