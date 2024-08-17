using Domain.Common.Services;

using SharedKernel;

namespace Domain.Usuarios.ValueObjects
{
    public class Password : ValueObject
    {
        public static readonly int MAXIMO_LENGTH = 16;
        public static readonly int MIN_LENGTH = 8;

        public string Value { get; private set; }
        public Password(string value)
        {
            Value = value;
        }

        private Password() { }

        static public Result<Password> Create(string value)
        {
            if (value.Length < MIN_LENGTH || value.Length > MAXIMO_LENGTH) return UsuariosFailures.LongitudDePasswordInvalida;
            if (StringUtils.ContieneEspaciosEnBlanco(value)) return UsuariosFailures.PasswordTieneEspaciosEnBlanco;
            return new Password()
            {
                Value = value
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] {
                Value
            };
        }
    }
}