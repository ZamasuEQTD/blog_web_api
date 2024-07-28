namespace Domain.Media.Abstractions
{
    public interface IResizer {
        Task<Stream> Resize(string Image, int width, int height);
    }
}