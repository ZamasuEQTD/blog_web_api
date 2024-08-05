using Domain.Usuarios;

namespace Domain.Comentarios
{
    public class Denuncia : Denuncias.Denuncia
    {
        public ComentarioId ComentarioId { get; private set; }
        public RazonDeDenuncia Razon { get; private set;}
        public Denuncia(UsuarioId denuncianteId, ComentarioId comentarioId, RazonDeDenuncia razon) : base(denuncianteId)
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