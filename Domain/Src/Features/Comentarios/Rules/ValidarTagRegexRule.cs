using Domain.Comentarios.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Comentarios
{
    public class ValidarTagRegexRule : IBusinessRule {

        private readonly string _tag;

        public ValidarTagRegexRule(string tag)
        {
            _tag = tag;
        }

        public string Message => "Tag invalido";

        public bool IsBroken() => !Tag.EsTagValido(_tag);
    }
}