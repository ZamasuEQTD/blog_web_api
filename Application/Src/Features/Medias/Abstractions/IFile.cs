namespace Application.Medias.Abstractions;

public interface IFile {
    public string FileName { get; }
    public Stream Stream { get; }
    public string ContentType { get; }
    public string Extension { get; }
    public FileType Type { get; }
}

public enum FileType {
    Video,
    Imagen,
    Gif,
    Desconocido
}