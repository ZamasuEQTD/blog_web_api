using Domain.Comentarios;
using Domain.Hilos.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules {

    public class ValidarTagUnicoEnHiloRule : IBusinessRule
    {

        private readonly InformacionComentario _informacionComentario;
        private readonly ConfiguracionDeComentarios _configuracionDeComentarios;

        public ValidarTagUnicoEnHiloRule(InformacionComentario informacionComentario, ConfiguracionDeComentarios configuracionDeComentarios) {
            _informacionComentario = informacionComentario;
            _configuracionDeComentarios = configuracionDeComentarios;
        }

        public string Message => throw new NotImplementedException();
        public bool IsBroken()=> _informacionComentario.TagUnico is null && _configuracionDeComentarios.IdUnicoActivado;
    }
}