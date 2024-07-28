using Application.Abstractions.Data;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class HilosRepository : IHilosRepository
    {
        private readonly ApplicationDbContext _context;

        public HilosRepository(ApplicationDbContext  context)
        {
            _context = context;
        }

        public void Add(Hilo hilo)
        {
            _context.Hilos.Add(hilo);    
        }
        public Task<Hilo?> GetHiloById(HiloId id)
        {
            return _context.Hilos.FirstOrDefaultAsync(h=>  h.Id == id);
        }
    }
}