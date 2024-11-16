using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse> //ICommand is more generic than TCommand so that we should use the contravariance keyword
    where TResponse : notnull
;