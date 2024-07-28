namespace Domain.Media.Abstractions
{
    public interface IVistaPreviaService {
        Stream GenerarDesdeVideo(string path);
    }
}