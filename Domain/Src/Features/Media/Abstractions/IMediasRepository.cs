namespace Domain.Medias.Abstractions
{

    public interface IMediasRepository
    {
        Task<HashedMediaProvider?> GetHashedMedia(string hash);
        void Add(Media media);
        void Add(MediaReference media);
        void Add(HashedMediaProvider media);

    }
}