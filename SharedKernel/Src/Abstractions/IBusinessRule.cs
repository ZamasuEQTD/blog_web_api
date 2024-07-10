namespace SharedKernel.Abstractions
{

    public interface IValidationHandler  {
        IValidationHandler  SetNext(IValidationHandler  validator);
        public Result Handle();
    }
    public abstract class ValidationHandler : IValidationHandler
    {
        private IValidationHandler? _next;

        public IValidationHandler SetNext(IValidationHandler  handler) => _next = handler;

        public virtual Result Handle() {
            return _next?.Handle()?? Result.Success();
        }
    }

    

    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}