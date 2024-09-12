namespace Application.Medias.Abstractions
{
    public interface IVideoPrevisualizadorGenerador
    {
        Stream GenerarDesdeVideo(string path);
    }
}