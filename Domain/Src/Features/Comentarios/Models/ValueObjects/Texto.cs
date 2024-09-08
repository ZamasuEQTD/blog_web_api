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
            if (value.Length < MIN || value.Length > MAX) return ComentariosFailures.LongitudDeTextoInvalido;

            if (TagUtils.CantidadDeTags(value) > 5) return ComentariosFailures.MaximaCantidadDeTaggueosSuperados;

            return new Texto(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }
}


