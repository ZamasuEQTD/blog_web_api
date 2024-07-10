using Domain.Encuestas.Failures;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Encuestas.Rules
{
    public class RespuestaDebeExistirRule :  ValidationHandler
    {
        private readonly Encuesta _encuesta;
        private readonly RespuestaId _respuestaId;

        public RespuestaDebeExistirRule(RespuestaId respuestaId, Encuesta encuesta)
        {
            _respuestaId = respuestaId;
            _encuesta = encuesta;
        }

        public override Result Handle()
        {
            if(!_encuesta.ContieneRespuesta(_respuestaId)){
                return EncuestasFailures.RESPUESTA_INEXISTENTE;
            }

            return base.Handle();
        }
    }
}