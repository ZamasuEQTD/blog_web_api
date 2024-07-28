namespace Domain.Media.Abstractions {
    public interface IUrlStrategy {
        public Task<Media> Procesar(string url);
    }

    public class YoutubeStrategy : IUrlStrategy {

        private readonly IMediasRepository _repository;

        public YoutubeStrategy(IMediasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Media> Procesar(string url) {
            MediaId id = new(Guid.NewGuid());

            Imagen previsualizacion = new Imagen(
                id,
                MediaProvider.Network(YoutubeUtils.GetVideoThumbnailFromUrl(url)),
                id
            );

            _repository.Add(previsualizacion);

            return new Youtube(
                new(Guid.NewGuid()),
                previsualizacion.Id,
                previsualizacion.Id,
                MediaProvider.Network(url)
            );
        }
    }
}
