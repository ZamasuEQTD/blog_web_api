using Domain.Hilos;

namespace Domain.Comentarios.Abstractions
{
    public interface IComentariosRepository {
        public Task<Comentario?> GetComentarioById(ComentarioId id);
        public Task<int> GetCantidadDeComentariosDestacados(HiloId hilo);
        public void Add(Comentario comentario);
    }
}