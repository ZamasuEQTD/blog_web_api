namespace Domain.Media.Abstractions
{
    public interface IMediasRepository
    {
        public void Add(MediaReference mediaReference);
        public void Add(Media media);
    
    }


}