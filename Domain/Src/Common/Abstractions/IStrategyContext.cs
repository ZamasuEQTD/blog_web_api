using Microsoft.Extensions.DependencyInjection;

namespace Domain.Abstractions
{
    public interface IStrategyContext  {
        Task<TOut> ExecuteStrategy<TKey, TIn, TOut, TStrategy>(TKey key, TIn model, CancellationToken cancellationToken) where TStrategy : IStrategy<TIn, TOut>;
    }

    public class StrategyContext(IServiceProvider serviceProvider) : IStrategyContext
    {
        public virtual async Task<TOut> ExecuteStrategy< TKey, TIn, TOut, TStrategy>(TKey key, TIn model, CancellationToken cancellationToken) where TStrategy : IStrategy<TIn, TOut>
        {
            ArgumentNullException.ThrowIfNull(model);

            TStrategy? strategy = serviceProvider.GetKeyedService<TStrategy>(key);

            return await strategy!.Execute(model, cancellationToken);
        }
    }

   

} 