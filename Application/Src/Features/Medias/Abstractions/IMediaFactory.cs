namespace  Application.Medias.Abstractions;

public interface IMediaFactory
{
    IMediaService Create(FileType tipo);
}
