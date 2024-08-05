using System.Text.RegularExpressions;
using Domain.Comentarios.Rules;
using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class TagUnico:ValueObject {
        static public readonly string RegexExp= "^[A-Z0-9]{"+Length+"}";
        static public readonly int Length = 3;
        public string Value {get;private set;}

        private TagUnico(string value)
        {
            Value = value;
        }

        public static TagUnico Create(string tag){
            CheckRule(new ValidarTagUnicoRegexRule(tag));
            return new TagUnico(tag);
        }

        static public bool EsTagValido(string tag) => Regex.IsMatch(tag,RegexExp);
        static public bool EsTagInvalido(string tag)=> !EsTagValido(tag);

        protected override IEnumerable<object> GetAtomicValues()
        {
            return  [Value];
        }
 
    }
}