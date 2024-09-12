namespace Domain.Media.Abstractions
{
    public interface IFileService
    {
        Task GuardarArchivo(Stream stream, string path);
    }
}