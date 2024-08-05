using System.Runtime.InteropServices;
using Domain.Media.Abstractions;
using MediatR;

namespace Domain.Media.Services.Strategies
{
    public class VideoProcesorStrategy : IMediaProcesorStrategy
    {
        private readonly MiniaturaProcesor _miniaturaProcesor;
        private readonly PrevisualizacionProcesor _previsualizacionProcesor;
        public async Task<Media> Execute(MediaProcesorParams request, CancellationToken cancellationToken)
        {
            MediaProvider previsualizacion = MediaProvider.File(
                await _previsualizacionProcesor.Procesar(request.Stream)
            );

            MediaProvider miniatura = MediaProvider.File(
                await _miniaturaProcesor.Procesar(previsualizacion.Path)
            );

            return new Video(
                previsualizacion,
                MediaProvider.File(request.Path)
            );
        }
    }
}