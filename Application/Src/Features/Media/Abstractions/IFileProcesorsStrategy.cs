using Domain.Media;

namespace Application.Medias.Abstractions
{

    public record FileProcesorParams(string Media, string File, string Hash);
    public interface IFileProcesorsStrategy
    {
        Task<FileMedia> Procesar(FileProcesorParams @params);
    }
}