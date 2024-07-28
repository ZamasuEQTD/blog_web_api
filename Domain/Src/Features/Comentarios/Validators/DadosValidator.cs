using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
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
    
    public class DadosRequeridosValidator :ValidationHandler{
        private readonly ConfiguracionDeComentarios configuracion;
        private readonly Dados? dados;
        public DadosRequeridosValidator(ConfiguracionDeComentarios configuracion, Dados? dados)
        {
            this.configuracion = configuracion;
            this.dados = dados;
        }

        public override Result Handle()
        {
            if(dados is null && configuracion.Dados){
                return ComentariosFailures.DADOS_REQUERIDOS;
            }
            if(dados is not null && !configuracion.Dados){
                return ComentariosFailures.DADOS_NO_REQUERIDOS;
            }
            return base.Handle();
        }
    }
}