using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Hilos;

namespace Persistence.Repositories {
    public class ComentariosRepository : IComentariosRepository
    {
        public void Add(Comentario comentario)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCantidadDeComentariosDestacados(HiloId hilo)
        {
            throw new NotImplementedException();
        }

        public Task<Comentario?> GetComentarioById(ComentarioId id)
        {
            throw new NotImplementedException();
        }
    }
}