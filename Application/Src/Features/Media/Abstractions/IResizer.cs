namespace Application.Medias.Abstractions
{
    public interface IResizer
    {
        Task<Stream> Resize(string path, int width, int height);
        Task<Stream> Resize(Stream image, int width, int height);


    }
}