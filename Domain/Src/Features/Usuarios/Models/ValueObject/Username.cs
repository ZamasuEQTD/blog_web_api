using System.Text.RegularExpressions;
using Domain.Common.Services;
using Domain.Usuarios.Rules;
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

    public abstract class TextoSinEspaciosEnBlancoRule : IBusinessRule
    {
        public string _message;
        public string Message => _message;
        private readonly string _text;
        public TextoSinEspaciosEnBlancoRule(string message, string text)
        {
            _message = message;
            _text = text;
        }

        public bool IsBroken() => StringUtils.ContieneEspaciosEnBlanco(_text);
    }

    public static class UsuariosFailures
    {
        public static readonly Error UsernameTieneEspaciosEnBlanco = new Error("");
        public static readonly Error LongitudDeUsernameInvalida = new Error("");
        public static readonly Error LongitudDePasswordInvalida = new Error("");
        public static readonly Error PasswordTieneEspaciosEnBlanco = new Error("");
        public static readonly Error UsernameOrPasswordIncorrecta = new Error("");

    }

}