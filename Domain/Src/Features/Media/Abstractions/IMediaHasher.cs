namespace Domain.Medias.Abstractions
{
    public interface IMediaHasher
    {
        Task<string> HashStream(Stream stream);
        Task<string> HashString(string url);
    }
}