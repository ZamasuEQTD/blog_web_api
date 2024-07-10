using Domain.Usuarios.Validations;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios.ValueObjects
{
    public class Password : ValueObject {
        public string Value { get; private set; }
        public Password(string value) {
            Value = value;
        }
        private Password(){}

        static public Result<Password> Create(string value) {
            ValidationHandler handler = new ValidarLargoDePasswordHandler(value);
            handler.SetNext(new PasswordSinEspaciosEnBlancos(value));
            
            Result result = handler.Handle();
            
            if(result.IsFailure) return result.Error;

            return new Password(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}