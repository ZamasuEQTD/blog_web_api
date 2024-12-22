using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public class UsuarioId : EntityId, IEquatable<UsuarioId> {
        private UsuarioId(){}
        public UsuarioId(Guid id) : base(id) { }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? other)
        {
            return base.Equals(other);
        }
        public bool Equals(UsuarioId? other)
        {
            return base.Equals(other);
        }
    }
    
}