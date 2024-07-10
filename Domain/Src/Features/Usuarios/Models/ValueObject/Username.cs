using System.Text.RegularExpressions;
using Domain.Usuarios.Failures;
using Domain.Usuarios.Validations;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios{
    public class Username : ValueObject
    {
        public static readonly int MAXIMO_LENGTH = 16;
        public static readonly int MIN_LENGTH = 8;

        public string Value { get; private set;}
        private Username (){}

        static public Result<Username> Create(string username){
            IValidationHandler handler = new ValidarLargoDeUsernameHandler(username);
            handler.SetNext(new ValidarEspaciosEnBlanco(username));

            Result result = handler.Handle();

            if(result.IsFailure){
                return result.Error;
            }

            return  new Username(){
                Value = username
            };
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}