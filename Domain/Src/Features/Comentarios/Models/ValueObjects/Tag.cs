using System.Text.RegularExpressions;
using Domain.Comentarios.Validators;
using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class Tag : ValueObject
    {
        static public readonly int LENGTH = 8;
        static public readonly string REGEX_STRING = "[A-Z0-9]{8}";
        static private readonly string TAG_REGEX_STRING = $"^{REGEX_STRING}$";
        public string Value { get; private set; }

        private Tag(){}
        private Tag(string value)
        {
            Value = value;
        }

        static public Result<Tag> Create(string tag)
        {
            Result result = new TagValidator(tag).Handle();

            if(result.IsFailure){
                return result.Error;
            }

            return new Tag(tag);
        }

        static public bool EsTagValido(string tag) => Regex.IsMatch(tag,TAG_REGEX_STRING);
        protected override IEnumerable<object> GetAtomicValues() {
            return new List<object>(){
                Value
            };
        }
    }
}