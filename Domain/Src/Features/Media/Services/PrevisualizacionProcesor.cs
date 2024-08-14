using System.Diagnostics.Contracts;
using Domain.Medias.Abstractions;

namespace Domain.Medias.Services
{
    public class PrevisualizacionProcesor
    {
        private readonly IFolderProvider _folderProvider;
        private readonly IPrevisualizacionVideoGenerador _previsualizacionVideoGenerador;
        private readonly IFileService _fileService;

        public PrevisualizacionProcesor(IFileService fileService, IPrevisualizacionVideoGenerador previsualizacionVideoGenerador, IFolderProvider folderProvider)
        {
            _fileService = fileService;
            _previsualizacionVideoGenerador = previsualizacionVideoGenerador;
            _folderProvider = folderProvider;
        }

        public async Task<string> Procesar(Stream video)
        {
            string previsualizacion_path = _folderProvider.VistasPrevias + "/" + Guid.NewGuid() + ".png";

            Stream vista_previa = _previsualizacionVideoGenerador.Generar(video);

            await _fileService.GuardarArchivo(vista_previa, previsualizacion_path);

            return previsualizacion_path;
        }
    }

    public interface IPrevisualizacionVideoGenerador
    {
        Stream Generar(Stream path);
    }
}