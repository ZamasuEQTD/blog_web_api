using Domain.Comentarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos.ValueObjects
{
    public class Titulo : ValueObject
    {
        static public readonly int MAX = 255;
        static public readonly int MIN = 20;

        public string Value { get; private set; }
        public Titulo() { }
        private Titulo(string value)
        {
            Value = value;
        }

        static public Result<Titulo> Create(string value)
        {
            if (value.Length > MAX || value.Length < MIN) return HilosFailures.LongitudDeTituloInvalida;

            return new Titulo(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }

    public class TituloDebeRespetarLongitudRule : IBusinessRule
    {
        private readonly string _descripcion;

        public TituloDebeRespetarLongitudRule(string descripcion)
        {
            _descripcion = descripcion;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => _descripcion.Length > Titulo.MAX || _descripcion.Length < Titulo.MIN;
    }
}