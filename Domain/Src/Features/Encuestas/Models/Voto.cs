using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Encuestas {
    public class Voto : Entity<VotoId>
    {
        public UsuarioId VotanteId {get; private set;}
        public RespuestaId RespuestaId {get; private set;}
        
        private Voto(){}
     
        public Voto(  UsuarioId votanteId, RespuestaId respuestaId) : base()
        {
            Id = new (Guid.NewGuid());
            VotanteId = votanteId;
            RespuestaId = respuestaId;
        }
    }
}