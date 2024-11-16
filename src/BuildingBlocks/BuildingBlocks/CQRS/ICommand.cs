using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand : ICommand<Unit>;

// Covariance allows assignments of derived type of TResponse to TResponse
//Which means: ICommand<TResponse> res = null; ICommand<TResponseSuperClass> sup = res is allowed
public interface ICommand<out TResponse> 
    : IRequest<TResponse> where TResponse : notnull;