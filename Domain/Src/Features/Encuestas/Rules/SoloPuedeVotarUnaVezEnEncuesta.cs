using Domain.Encuestas.Failures;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Encuestas.Rules
{
    public class UsuarioSoloPuedeVotarUnaVezEnEncuestaRule : ValidationHandler
    {
        private readonly UsuarioId _usuarioId;
        private readonly Encuesta _encuesta;

        public UsuarioSoloPuedeVotarUnaVezEnEncuestaRule(UsuarioId usuarioId, Encuesta encuesta)
        {
            _usuarioId = usuarioId;
            _encuesta = encuesta;
        }

        public override Result Handle()
        {
            if(_encuesta.HaVotado(_usuarioId)){
                return EncuestasFailures.YA_HA_VOTADO;
            }

            return base.Handle();
        }
    }
}