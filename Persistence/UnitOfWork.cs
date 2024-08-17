using Application.Abstractions.Data;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BlogDbContext _context;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}