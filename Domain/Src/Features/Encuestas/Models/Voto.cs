using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class Voto : Entity<VotoId>
    {
        public UsuarioId VotanteId {get; private set;}
        public EncuestaId EncuestaId {get; private set;}
        public RespuestaId RespuestaId {get; private set;}
        private Voto(){}
     
        public Voto(VotoId id, UsuarioId votanteId, RespuestaId respuestaId) : base(id)
        {
            Id = id;
            VotanteId = votanteId;
            RespuestaId = respuestaId;
        }
    }
}