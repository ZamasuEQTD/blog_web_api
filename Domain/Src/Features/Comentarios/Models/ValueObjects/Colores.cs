using System.Drawing;
using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class Colores : ValueObject
    {
        public string Value { get; private set; }

        private Colores(string value)
        {
            Value = value;
        }

        static public readonly Colores Multi = new Colores("Multi");
        static public readonly Colores Invertido = new Colores("Invertido");
        static public readonly Colores Rojo = new Colores("Rojo");
        static public readonly Colores Amarillo = new Colores("Amarillo");
        static public readonly Colores Azu = new Colores("Azu");

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}