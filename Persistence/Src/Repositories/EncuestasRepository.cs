using Application.Abstractions.Data;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class EncuestasRepository : IEncuestasRepository {
        private readonly ApplicationDbContext _context;
        public EncuestasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Encuesta encuesta)
        {
            _context.Encuestas.Add(encuesta);
        }

        public Task<Encuesta?> GetEncuestaById(EncuestaId id) {
            return _context.Encuestas.FirstOrDefaultAsync(e=> e.Id == id);
        }
    }
}