namespace Application.Medias.Abstractions;

public interface IFileService
{
    Task GuardarArchivo(Stream stream, string path);
}

