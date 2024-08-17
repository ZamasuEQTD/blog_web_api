using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos.ValueObjects
{
    public class Descripcion : ValueObject
    {
        static public readonly int MAX = 255;
        static public readonly int MIN = 10;

        public string Value { get; private set; }
        public Descripcion() { }
        private Descripcion(string value)
        {
            Value = value;
        }

        static public Result<Descripcion> Create(string value)
        {
            if (value.Length > MAX || value.Length < MIN) return HilosFailures.LongitudDeDescripcionInvalida;

            return new Descripcion(value);
        }

        protected override IEnumerable<object> GetAtomicValues() => [
            this.Value
        ];
    }
}