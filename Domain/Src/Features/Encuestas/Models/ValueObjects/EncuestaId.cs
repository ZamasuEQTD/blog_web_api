using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class EncuestaId : EntityId
    {
        public EncuestaId(Guid id) : base( id ){}
        private EncuestaId(){}
    }
}