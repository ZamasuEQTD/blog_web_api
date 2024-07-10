using MediatR;
using SharedKernel;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class LogginPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
    {
        private readonly ILogger<LogginPipelineBehavior<TRequest, TResponse>> _logger;

        public LogginPipelineBehavior(ILogger<LogginPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation(
                "Comenzando request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name, DateTime.UtcNow
            );
            
            var result = await next();

            if(result.IsFailure){
                _logger.LogError(
                    "Request ha fallado {@RequestName}, {@Error} {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error.Code, 
                    DateTime.UtcNow
                );
            }

            _logger.LogInformation(
                "Terminado request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name, DateTime.UtcNow
            );
            return result;
        }
    }
}