namespace Domain.Media.Abstractions.Strategies {
    public interface IMediaProcesorsStrategy {
        Task<Media> Procesar(string media);
    }
}