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
        static public readonly Colores Azul = new Colores("Azul");
        static public readonly Colores Verde = new Colores("Verde");
        static public readonly Colores Black = new Colores("Black");
        static public readonly Colores White = new Colores("White");

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };
        }


    }
}