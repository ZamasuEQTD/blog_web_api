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
            await _miniaturaProcesor.Procesar(@params.Media, @params.Hash);

            return new Imagen(
                @params.Hash,
                @params.Media,
                @params.File
            );
        }
    }
}