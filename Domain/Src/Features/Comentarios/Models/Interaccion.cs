using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Comentarios
{
    public class RelacionDeComentario : Entity<InteraccionId>
    {
        public ComentarioId ComentarioId { get; private set; }
        public UsuarioId UsuarioId { get; private set; }
        public bool Oculto { get; private set; }

        public RelacionDeComentario(ComentarioId comentarioId, UsuarioId usuarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.ComentarioId = comentarioId;
            this.UsuarioId = usuarioId;
            this.Oculto = false;
        }

        public void Ocultar()
        {
            this.Oculto = !this.Oculto;
        }
    }

    public class InteraccionId : EntityId
    {
        public InteraccionId(Guid id) : base(id) { }
    }
}