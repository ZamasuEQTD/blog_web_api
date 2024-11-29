using Application.Medias.Abstractions;
using Domain.Media;

namespace Application.Medias.Services
{
    public class GifProcesador : IFileProcesorsStrategy
    {
        private readonly MiniaturaProcesor _miniaturaProcesor;
        private readonly GifVideoPrevisualizadorProcesador _GifVideoPrevisualizadorProcesador;

        public GifProcesador(GifVideoPrevisualizadorProcesador GifVideoPrevisualizadorProcesador, MiniaturaProcesor miniaturaProcesor)
        {
            _GifVideoPrevisualizadorProcesador = GifVideoPrevisualizadorProcesador;
            _miniaturaProcesor = miniaturaProcesor;
        }

        public async Task<FileMedia> Procesar(FileProcesorParams @params)
        {
            using Stream stream = _GifVideoPrevisualizadorProcesador.GenerarStream(@params.Media);

            await _miniaturaProcesor.Procesar(stream, @params.Hash);

            return new Gif(
                @params.Hash,
                @params.Media,
                @params.File
            );
        }
    }
}