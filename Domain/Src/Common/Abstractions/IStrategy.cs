using MediatR;

namespace Domain.Abstractions 
{
    public interface IStrategy<in TIn, TOut> {
        public Task<TOut> Execute(TIn request, CancellationToken cancellationToken);
    }

    public class ProcesarMediaParams
    {

    }

    public interface IProcesarMedia : IStrategy<ProcesarMediaParams,Unit> {

    }

    public class ProcesarVideo : IProcesarMedia {
        public Task<Unit> Execute(ProcesarMediaParams input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}