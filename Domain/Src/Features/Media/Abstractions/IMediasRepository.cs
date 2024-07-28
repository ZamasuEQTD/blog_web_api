namespace Domain.Media.Abstractions
{
    public interface IMediasRepository {

        public Task<HashedMedia?> GetHashedMediaByHash(string hash);
        public void Add(MediaReference mediaReference);
        public void Add(MediaSource mediaReference);
        public void Add(Media media);
    
    }


}