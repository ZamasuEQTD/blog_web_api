using FluentValidation;
using MediatR;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Behaviors
{
    public class ValidationPipeLineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result {
       
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeLineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Count() == 0){
                return await next();
            }
            
            Error[] errors = _validators
            .Select(v=> v.Validate(request))
            .SelectMany(r=>r.Errors)
            .Where(f=> f is not null)
            .Select(f=> new Error(
                f.PropertyName,
                f.ErrorMessage
            ))
            .Distinct().ToArray();

            if(errors.Length != 0) {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }
    
        private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result {
            if(typeof(TResult) == typeof(Result)){
                return( ValidationResult.WithErrors(errors) as TResult)!;
            }

            object ob = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[]{errors})!;
        
            return (TResult) ob;
        }

    }


}