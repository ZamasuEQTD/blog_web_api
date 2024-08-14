using MediatR;
using SharedKernel.Abstractions;

namespace Domain.Abstractions
{
    public interface IStrategy<in TIn, TOut>
    {
        public Task<TOut> Execute(TIn request, CancellationToken cancellationToken);
    }
}