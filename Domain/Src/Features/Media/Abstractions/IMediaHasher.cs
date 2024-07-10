namespace Domain.Media.Abstractions
{
    public interface IMediaHasher
    {
        public Task<string> HashStream(Stream stream);
        public Task<string> HashString(string url);
    }
}