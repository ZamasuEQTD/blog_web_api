using Domain.Hilos;
using Domain.Media;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data
{
    public interface  IApplicationDbContext {
        public DbSet<Hilo> Hilos{ get; set; }
        public DbSet<MediaReference> MediaReferences { get; set; }
        // public DbSet<Media> Medias { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}