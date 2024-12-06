namespace Application.Medias.Abstractions;

public interface IHasher
{
    Task<string> Hash(Stream stream);
    Task<string> Hash(string url);
}