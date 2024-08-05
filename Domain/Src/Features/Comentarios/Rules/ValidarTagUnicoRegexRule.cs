using Domain.Comentarios.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Rules {
    public class ValidarTagUnicoRegexRule : IBusinessRule {
        private readonly string _tag;

        public ValidarTagUnicoRegexRule(string tag)
        {
            _tag = tag;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => TagUnico.EsTagInvalido(_tag);
    }
}