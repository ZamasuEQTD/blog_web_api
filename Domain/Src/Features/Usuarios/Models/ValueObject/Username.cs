using System.Text.RegularExpressions;
using Domain.Common.Services;

using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public class Username : ValueObject
    {
        public static readonly int MAXIMO_LENGTH = 16;
        public static readonly int MIN_LENGTH = 8;

        public string Value { get; private set; }

        private Username() { }

        static public Result<Username> Create(string value)
        {
            if (value.Length < MIN_LENGTH || value.Length > MAXIMO_LENGTH) return UsuariosFailures.LongitudDeUsernameInvalida;

            if (StringUtils.ContieneEspaciosEnBlanco(value)) return UsuariosFailures.UsernameTieneEspaciosEnBlanco;

            return new Username()
            {
                Value = value
            };
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}