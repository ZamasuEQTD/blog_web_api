using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class ComentarioStatus : ValueObject
    {
        public string Value { get; private set; }

        private ComentarioStatus(string value)
        {
            Value = value;
        }

        public static ComentarioStatus Activo => new ComentarioStatus("Activo");
        public static ComentarioStatus Eliminado => new ComentarioStatus("Eliminado");

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[]{
                this.Value
            };
        }
    }
}