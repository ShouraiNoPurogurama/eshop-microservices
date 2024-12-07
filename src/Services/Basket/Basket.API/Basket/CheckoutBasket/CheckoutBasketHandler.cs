using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(b => b.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required.");
        RuleFor(b => b.BasketCheckoutDto).NotNull().WithMessage("Basket Checkout cannot be null.");
    }
} 

public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    // Get existing basket with total price
    // Set TotalPrice on BasketCheckoutMessage
    // Send BasketCheckoutMessage to RabbitMQ using MassTransit
    // Delete the Basket
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken)
                     ?? throw new BasketNotFoundException(request.BasketCheckoutDto.UserName);

        var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(basket.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}