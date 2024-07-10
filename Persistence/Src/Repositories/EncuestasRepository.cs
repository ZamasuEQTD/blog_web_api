using Application.Abstractions.Data;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;

namespace Persistence.Repositories
{
    public class EncuestasRepository : IEncuestasRepository
    {

        private readonly IApplicationDbContext _context;

        public EncuestasRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Encuesta encuesta)
        {
            throw new NotImplementedException();
        }
    }
}