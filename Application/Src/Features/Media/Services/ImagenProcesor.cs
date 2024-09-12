using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;

namespace Application.Medias.Services
{
    public class ImagenProcesor : IFileProcesorsStrategy
    {
        private readonly MiniaturaProcesor _miniaturaProcesor;

        public ImagenProcesor(MiniaturaProcesor miniaturaProcesor)
        {
            _miniaturaProcesor = miniaturaProcesor;
        }


        public async Task<FileMedia> Procesar(FileProcesorParams @params)
        {
            return new Imagen(
                @params.Hash,
                @params.Media,
                @params.File,
                await _miniaturaProcesor.Procesar(@params.Media)
            );
        }
    }
}