using Domain.Media.Abstractions;
using Domain.Media.Abstractions.Strategies;

namespace Domain.Media.Services {
    public class VideoProcesorStrategy : IMediaProcesorsStrategy {
        private readonly PrevisualizadorProcesor _previsualizadorProcesor;
        private readonly IMediasRepository _mediasRepository;

        public VideoProcesorStrategy(PrevisualizadorProcesor previsualizadorProcesor, MiniaturaProcesor miniaturaProcesor, IMediasRepository mediasRepository)
        {
            _previsualizadorProcesor = previsualizadorProcesor;
            _mediasRepository = mediasRepository;
        }

        public async Task<Media> Procesar(string path) {
            Imagen previsualizacion = await _previsualizadorProcesor.Procesar(path);

            _mediasRepository.Add(previsualizacion);

            return new Video(
                new(Guid.NewGuid()),
                previsualizacion.Id,
                previsualizacion.Thumbnail!,
                MediaProvider.File(
                    path
                )
            ); 
        }
    }
}