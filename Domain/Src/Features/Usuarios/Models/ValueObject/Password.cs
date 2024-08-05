using Domain.Usuarios.Rules;
using SharedKernel;

namespace Domain.Usuarios.ValueObjects
{
    public class Password : ValueObject {
        public string Value { get; private set; }
        public Password(string value) {
            Value = value;
        }
        
        private Password(){}

        static public Password Create(string value) {
            CheckRule(new PasswordSinEspaciosEnBlancoRule(value));
            CheckRule(new LargoDePasswordValidoRule(value));

            return new Password(){
                Value = value
            };
        }

        protected override IEnumerable<object> GetAtomicValues() {
            return new [] { 
                Value
            };
        }
    }
}