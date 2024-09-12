using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Media.ValueObjects;

namespace Application.Medias.Abstractions
{
    public interface IFileContextStrategy
    {
        Task<FileMedia> Execute(FileType type, FileProcesorParams @params);
    }
}