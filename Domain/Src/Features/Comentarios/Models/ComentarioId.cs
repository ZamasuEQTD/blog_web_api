using SharedKernel.Abstractions;

namespace Domain.Comentarios
{ 
    public class ComentarioId : EntityId
    {
        private ComentarioId (){}
        public ComentarioId(Guid id) : base(id) { }
    }
    
}