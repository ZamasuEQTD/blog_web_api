using Application.Abstractions.Data;
using Domain.Hilos;
using Domain.Hilos.Abstractions;

namespace Persistence.Repositories
{
    public class HilosRepository : IHilosRepository
    {
        private readonly IApplicationDbContext _context;

        public HilosRepository(IApplicationDbContext  context)
        {
            _context = context;
        }

        public void Add(Hilo hilo)
        {
            _context.Hilos.Add(hilo);    
        }

        public Task<Hilo> GetHiloById(Hilo.HiloId id)
        {
            throw new NotImplementedException();
        }
    }
}