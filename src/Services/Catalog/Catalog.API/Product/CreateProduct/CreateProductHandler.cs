using MediatR;

namespace Catalog.API.Product.CreateProduct;

public record CreateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}