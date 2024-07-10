namespace Domain.Comentarios.Abstractions
{
    public interface IComentariosRepository {
        public Task<Comentario?> GetComentarioById(ComentarioId id);
        public void Add(Comentario comentario);
    }
}