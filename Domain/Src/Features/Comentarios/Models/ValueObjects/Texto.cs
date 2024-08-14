using Domain.Comentarios.Services;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.ValueObjects
{
    public class Texto : ValueObject
    {
        static public readonly int MAX = 255;
        static public readonly int MIN = 20;

        public string Value { get; private set; }
        public Texto() { }
        private Texto(string value)
        {
            Value = value;
        }

        static public Result<Texto> Create(string value)
        {
            if (value.Length > MIN || value.Length > MAX) return ComentariosFailures.LongitudDeTextoInvalido;

            if (TagUtils.CantidadDeTags(value) > 5) return ComentariosFailures.MaximaCantidadDeTaggueosSuperados;

            return new Texto(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }

    public class TextoNoPuedeTagguearMasDe5ComentariosRule : IBusinessRule
    {
        private readonly string _texto;

        public TextoNoPuedeTagguearMasDe5ComentariosRule(string texto)
        {

            _texto = texto;
        }

        public string Message => "No puedes tagguear mas de 5 comentarios";

        public bool IsBroken() => TagUtils.CantidadDeTags(_texto) > 5;
    }

    public class TextoDebeRespetarLongitudRule : IBusinessRule
    {
        private readonly string _texto;

        public TextoDebeRespetarLongitudRule(string texto)
        {
            _texto = texto;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => _texto.Length > Texto.MAX || _texto.Length < Texto.MIN;
    }
}


