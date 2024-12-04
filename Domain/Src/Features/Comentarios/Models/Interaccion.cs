using Domain.Hilos;
using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Comentarios
{
    public class ComentarioInterracion : Entity<RelacionId>
    {
        public ComentarioId ComentarioId { get; private set; }
        public UsuarioId UsuarioId { get; private set; }
        public bool Oculto { get; private set; }
        private ComentarioInterracion() { }
        public ComentarioInterracion(ComentarioId comentarioId, UsuarioId usuarioId)
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
}