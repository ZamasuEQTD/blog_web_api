using MediatR;
using SharedKernel;

namespace Application.Abstractions.Messaging
{
    public interface ICommand : IRequest
    {

    }
    public interface ICommand<TResponse> : IRequest< TResponse>
    {

    }
}