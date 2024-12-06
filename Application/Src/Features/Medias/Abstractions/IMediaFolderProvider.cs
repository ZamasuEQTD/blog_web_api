namespace Application.Medias.Abstractions;

public interface IMediaFolderProvider
{
    string ThumbnailFolder { get; }
    string FilesFolder { get; }
    string Previsualizaciones { get; }
}