using Domain.Comentarios.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Rules
{
    public class ValidarDadosRule : IBusinessRule {
        private readonly int _value;

        public ValidarDadosRule(int value)
        {
            _value = value;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => Dados.ValorEsInvalido(_value);
    }
}