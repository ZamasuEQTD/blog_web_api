using Domain.Denuncias;
using Domain.Usuarios;

namespace Domain.Comentarios
{
    public class DenunciaDeComentario : Denuncia
    {
        public ComentarioId ComentarioId { get; private set; }
        public RazonDeDenuncia Razon { get; private set; }
        private DenunciaDeComentario() { }
        public DenunciaDeComentario(UsuarioId denuncianteId, ComentarioId comentarioId, RazonDeDenuncia razon) : base(denuncianteId)
        {
            ComentarioId = comentarioId;
            Razon = razon;
        }

        public enum RazonDeDenuncia
        {
            Otro
        }
    }
}