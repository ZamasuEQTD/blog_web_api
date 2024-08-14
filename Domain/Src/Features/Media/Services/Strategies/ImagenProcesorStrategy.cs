using Domain.Medias.Abstractions;
using Domain.Medias.Services;

namespace Domain.Medias
{
    public class ImagenProcesorStrategy : IMediaProcesorStrategy
    {
        private readonly MiniaturaProcesor _miniaturaProcesor;

        public ImagenProcesorStrategy(MiniaturaProcesor miniaturaProcesor)
        {
            _miniaturaProcesor = miniaturaProcesor;
        }

        public async Task<Media> Execute(MediaProcesorParams request, CancellationToken cancellationToken)
        {
            return new Imagen(
                request.Path,
                await _miniaturaProcesor.Procesar(request.Path)
            );
        }
    }
}