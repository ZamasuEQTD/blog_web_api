using Domain.Comentarios;
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

        public Task<int> CantidadDeDestacados(HiloId hilo) => _context.Comentarios.CountAsync(c => c.Hilo == hilo && c.Destacado);

        public Task<Comentario?> GetComentarioById(ComentarioId id) => _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

        public Task<RelacionDeComentario?> GetRelacionDeComentario(UsuarioId usuario, ComentarioId comentario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HaDenunciado(ComentarioId id, UsuarioId usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}