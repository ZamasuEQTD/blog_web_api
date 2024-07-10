using Domain.Comentarios.Abstractions;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Usuarios;

namespace Domain.Comentarios.Services
{
    public class TagGenerator : ITagGenerator
    {
        public Tag GenerarTag()
        {
            throw new NotImplementedException();
        }

        public TagUnico GenerarTagUnico(Hilo.HiloId hiloId, UsuarioId usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}