using Domain.Media.Abstractions;

namespace Domain.Media.Services
{
    public class PrevisualizadorProcesor {
        private readonly IVistaPreviaService _vistaPreviaService;
        private readonly IFolderProvider _folderProvider;
        private readonly IFileService _fileService;
        private readonly MiniaturaProcesor _miniaturaProcesor;
        private readonly IMediasRepository _mediasRepository;
        public PrevisualizadorProcesor(IVistaPreviaService vistaPreviaService, IFolderProvider folderProvider, IFileService fileService, MiniaturaProcesor miniaturaProcesor, IMediasRepository mediasRepository)
        {
            _vistaPreviaService = vistaPreviaService;
            _folderProvider = folderProvider;
            _fileService = fileService;
            _miniaturaProcesor = miniaturaProcesor;
            _mediasRepository = mediasRepository;
        }

        public async Task<Imagen> Procesar(string path){
            string previsualizacion_path = _folderProvider.VistasPrevias + "/" + Guid.NewGuid() + ".png";

            Stream vista_previa = _vistaPreviaService.GenerarDesdeVideo(path);

            await _fileService.GuardarArchivo(vista_previa,previsualizacion_path) ;

            Imagen thumbnail = await _miniaturaProcesor.Procesar(previsualizacion_path);

            _mediasRepository.Add(thumbnail);

            return new Imagen(
                new(Guid.NewGuid()),
                MediaProvider.File(
                    previsualizacion_path
                ),
                thumbnail.Id
            ); 
        }
    }
}