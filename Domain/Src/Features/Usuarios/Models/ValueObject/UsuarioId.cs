using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public class UsuarioId : EntityId {
        private UsuarioId(){}
        public UsuarioId(Guid id) : base(id) { }
    }
    
}