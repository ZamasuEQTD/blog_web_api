using Domain.Comentarios;
using Domain.Hilos.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules {
    public class ValidarDadosEnHiloRule : IBusinessRule {
        private readonly InformacionComentario _informacionComentario;
        private readonly ConfiguracionDeComentarios _configuracionDeComentarios;
        public ValidarDadosEnHiloRule(InformacionComentario informacionComentario, ConfiguracionDeComentarios configuracionDeComentarios)
        {
            _informacionComentario = informacionComentario;
            _configuracionDeComentarios = configuracionDeComentarios;
        }

        public string Message  => throw new NotImplementedException();
        public bool IsBroken() => _informacionComentario.Dados is null && _configuracionDeComentarios.Dados;
    }
}