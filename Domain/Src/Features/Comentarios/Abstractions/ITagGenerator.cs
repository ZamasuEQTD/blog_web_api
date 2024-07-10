using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Usuarios;

namespace Domain.Comentarios.Abstractions
{
    public interface ITagGenerator {
        public Tag GenerarTag();
        public TagUnico GenerarTagUnico(Hilo.HiloId hiloId, UsuarioId usuarioId);
    }
}