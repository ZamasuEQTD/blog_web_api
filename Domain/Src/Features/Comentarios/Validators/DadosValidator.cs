using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Validators
{
    public class ValorDeDadosValidator : ValidationHandler {
        private readonly int _valor;

        public ValorDeDadosValidator(int valor)
        {
            _valor = valor;
        }

        public override Result Handle()
        {
            if(Dados.ValorEsInvalido(_valor)) return DadosFailures.VALOR_INVALIDO;

            return base.Handle();
        }

    }
}