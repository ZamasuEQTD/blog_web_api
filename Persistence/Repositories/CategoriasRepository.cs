using Domain.Categorias;
using Domain.Categorias.Abstractions;

namespace Persistence.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly BlogDbContext _context;
        public CategoriasRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Categoria categoria)
        {
            _context.Add(categoria);
        }

        public void Add(Subcategoria categoria)
        {
            _context.Add(categoria);
        }
    }
}