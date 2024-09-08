using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Denuncias;
using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ComentariosRepository : IComentariosRepository
    {

        private readonly BlogDbContext _context;
        public ComentariosRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Denuncia denuncia) => _context.Add(denuncia);
        public Task<Comentario?> GetComentarioById(ComentarioId id) => _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
        public Task<Comentario?> GetComentarioByTag(HiloId hiloId, Tag tag) => _context.Comentarios.FromSql($@"
                SELECT *
                FROM comentarios comentario
                WHERE comentario.hilo_id = {hiloId.Value}
                AND comentario.tag = {tag.Value}
            ").FirstOrDefaultAsync();
    }
}