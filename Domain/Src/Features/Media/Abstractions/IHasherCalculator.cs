namespace Domain.Media.Abstractions
{
    public interface IHasherCalculator
    {
        public Task<string> Hash(Stream stream);
        public Task<string> Hash(string url);
    }
}