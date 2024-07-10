using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class VotoId : EntityId 
    {
        public VotoId(Guid id) : base( id ){}
        private VotoId(){}
    }
}