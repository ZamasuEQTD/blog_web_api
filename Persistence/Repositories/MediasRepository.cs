using Application.Hilos.Commands;
using Domain.Media;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MediasRepository : IMediasRepository
    {
        private readonly BlogDbContext _context;

        public MediasRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(HashedMedia media) => _context.Add(media);

        public void Add(MediaReference reference) => _context.Add(reference);

        public Task<HashedMedia?> GetMediaByHash(string hash) => _context.Medias.FirstOrDefaultAsync(m => m.Hash == hash);
    }
}