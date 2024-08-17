using System.Text.RegularExpressions;
using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class TagUnico : ValueObject
    {
        static public readonly string RegexExp = "^[A-Z0-9]{3}$";
        public string Value { get; private set; }

        private TagUnico(string value)
        {
            Value = value;
        }

        public static Result<TagUnico> Create(string tag)
        {
            if (!EsTagValido(tag)) return ComentariosFailures.TagUnicoInvalido;

            return new TagUnico(tag);
        }

        static public bool EsTagValido(string tag) => Regex.IsMatch(tag, RegexExp);

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }

    }
}