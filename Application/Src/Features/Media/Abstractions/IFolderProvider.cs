namespace Application.Medias.Abstractions
{
    public interface IFolderProvider
    {
        string ThumbnailFolder { get; }
        string FilesFolder { get; }
        string Previsualizaciones { get; }
    }
}