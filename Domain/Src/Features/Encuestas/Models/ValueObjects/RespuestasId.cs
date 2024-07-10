using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class RespuestaId  : EntityId 
    {
        public RespuestaId(Guid id) : base( id ){}
        private RespuestaId(){}
    }
}