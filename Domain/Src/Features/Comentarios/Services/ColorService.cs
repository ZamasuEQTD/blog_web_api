using Domain.Categorias;
using Domain.Comentarios.ValueObjects;
using Domain.Core;

namespace Domain.Comentarios.Services
{
    static public class ColorService
    {
        static private readonly List<WeightValue<Colores>> colores = new List<WeightValue<Colores>>() {
            new WeightValue<Colores>(20,Colores.Amarillo),
            new WeightValue<Colores>(20,Colores.Azul),
            new WeightValue<Colores>(20,Colores.Rojo),
            new WeightValue<Colores>(1,Colores.Multi),
            new WeightValue<Colores>(1,Colores.Invertido),
        };

        public static Colores GenerarColor(SubcategoriaId subcategoria, List<SubcategoriaId> paranormales, DateTime now)
        {
            List<WeightValue<Colores>> colors = [.. colores];

            List<IColoresRule> rules = new List<IColoresRule>(){
                new HorarioParanormalRule(
                    now,
                    paranormales,
                    subcategoria
                )
            };

            foreach (var rule in rules)
            {
                if (rule.Matches(colors))
                {
                    rule.Apply(colors);
                }
            }

            return colors.Pick();
        }
    }

    public interface IRule<E>
    {
        bool Matches(E input);
        void Apply(E input);
    }

    public interface IColoresRule : IRule<List<WeightValue<Colores>>> { }

    public class HorarioParanormalRule : IColoresRule
    {
        private readonly DateTime now;
        private readonly List<SubcategoriaId> subcategorias;
        private readonly SubcategoriaId subcategoria;
        public HorarioParanormalRule(DateTime now, List<SubcategoriaId> subcategorias, SubcategoriaId subcategoria)
        {
            this.now = now;
            this.subcategorias = subcategorias;
            this.subcategoria = subcategoria;
        }

        public void Apply(List<WeightValue<Colores>> input)
        {
            input.Add(new WeightValue<Colores>(2, Colores.Black));
        }

        public bool Matches(List<WeightValue<Colores>> input)
        {
            return subcategorias.Contains(subcategoria) && EsHoraParanormal();
        }

        private bool EsHoraParanormal()
        {
            return now.Hour > 22 || now.Hour < 5;
        }
    }
}