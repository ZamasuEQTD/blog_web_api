using Domain.Medias.Abstractions;

namespace Domain.Medias.Services.Strategies
{
    public class GifVideoProcesorStrategy : IMediaProcesorStrategy
    {
        private readonly MiniaturaProcesor _miniaturaProcesor;
        private readonly PrevisualizacionProcesor _previsualizacionProcesor;

        public GifVideoProcesorStrategy(PrevisualizacionProcesor previsualizacionProcesor, MiniaturaProcesor miniaturaProcesor)
        {
            _previsualizacionProcesor = previsualizacionProcesor;
            _miniaturaProcesor = miniaturaProcesor;
        }

        public async Task<Media> Execute(MediaProcesorParams request, CancellationToken cancellationToken)
        {
            string previsualizacion = await _previsualizacionProcesor.Procesar(request.Stream);

            string miniatura = await _miniaturaProcesor.Procesar(previsualizacion);

            return new Video(
                previsualizacion,
                miniatura,
                request.Path
            );
        }
    }
}