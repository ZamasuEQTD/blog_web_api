using Application.Abstractions.Data;
using Domain.Media;
using Domain.Media.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MediasRepository : IMediasRepository
    {
        private readonly ApplicationDbContext _context;

        public MediasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(MediaReference mediaReference)
        {
            _context.MediaReferences.Add(mediaReference);
        }

        public void Add(Media media)
        {
            _context.Medias.Add(media);
        }

        public void Add(MediaSource source)=> _context.Add(source);

        public Task<HashedMedia?> GetHashedMediaByHash(string hash)
        {
            return _context.MediaSources.OfType<HashedMedia>().FirstOrDefaultAsync(m=> m.Hash == hash);
        }

        public Task<Media?> GetMediaByHash(string hash)
        {
            throw new NotImplementedException();
        }

    }
}