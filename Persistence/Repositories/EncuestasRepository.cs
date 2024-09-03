using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class EncuestasRepository : IEncuestasRepository
    {

        private BlogDbContext _context;

        public EncuestasRepository(BlogDbContext context)
        {
            _context = context;
        }

        public Task<Encuesta> GetEncuestaById(EncuestaId id) => _context.Encuestas.FirstAsync(e => e.Id == id);
    }
}