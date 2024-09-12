using SharedKernel;

namespace Domain.Hilos.ValueObjects
{
    public class HiloStatus : ValueObject
    {
        public string Value { get; private set; }

        private HiloStatus(string value)
        {
            Value = value;
        }

        public static HiloStatus Activo => new HiloStatus("Activo");
        public static HiloStatus Archivado => new HiloStatus("Eliminar");
        public static HiloStatus Eliminado => new HiloStatus("Archivado");
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };
        }
    }
}