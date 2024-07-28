using Domain.Media.Abstractions.Strategies;

namespace Domain.Media.Services
{
    public class ImagenProcesorStrategy : IMediaProcesorsStrategy {
        private readonly MiniaturaProcesor _miniaturaProcesor;
        public ImagenProcesorStrategy(MiniaturaProcesor miniaturaProcesor)
        {
            _miniaturaProcesor = miniaturaProcesor;
        }

        public async Task<Media> Procesar(string path) {
            Imagen miniatura = await _miniaturaProcesor.Procesar(path);

            return new Imagen(
                new (Guid.NewGuid()),
                MediaProvider.File(
                    path
                ),
                miniatura.Id
            );
        }
    }
}