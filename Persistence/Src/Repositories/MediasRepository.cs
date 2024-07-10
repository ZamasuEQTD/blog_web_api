using Application.Abstractions.Data;
using Domain.Media;
using Domain.Media.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class MediasRepository : IMediasRepository
    {
        private readonly IApplicationDbContext _context;

        public MediasRepository(IApplicationDbContext context)
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

        public Task<Media?> GetMediaByHash(string hash)
        {
            return _context.Medias.FirstOrDefaultAsync(m=>m.Hash == hash);
        }

    }
}