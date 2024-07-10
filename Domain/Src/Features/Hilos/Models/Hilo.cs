using Domain.Comentarios;
using Domain.Encuestas;
using Domain.Media;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos {
    public class Hilo : Entity <Hilo.HiloId> {
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public HiloStatus Status {get; private set;}     
        public UsuarioId AutorId {get; private set; }
        public Usuario Autor {get; private set; }
        public MediaReferenceId PortadaId { get; private set; }
        public ConfiguracionDeComentarios Configuracion {get;private set;}
        public EncuestaId? EncuestaId {get; private set; }
        private Hilo(){}
        private Hilo(
            HiloId id,
            string titulo,
            string descripcion,
            UsuarioId autorId,
            MediaReferenceId portadaId,
            EncuestaId? encuestaId
        ) : base(id){
            Titulo = titulo;
            Descripcion = descripcion;
            PortadaId = portadaId;
            AutorId = autorId;
            EncuestaId = encuestaId;
            Status = HiloStatus.Activo;
        }

        static public  Hilo Create(
            HiloId id,
            UsuarioId autor,
            MediaReferenceId portadaId,
            EncuestaId? encuestaId,
            string titulo,
            string descripcion
        ){
            return new Hilo(
                id,
                titulo,
                descripcion,
                autor,
                portadaId,
                encuestaId
            );
        }
        public bool TieneEncuesta => EncuestaId is not null;
        public class HiloId : EntityId
        {
            public HiloId(Guid id):base(id){}
        }

        public enum HiloStatus {
            Activo,
            Eliminado,
            Archivado
        }

        internal Result Eliminar() {
            ValidationHandler handler = new HiloNoPuedeEstarEliminado(this);
            
            Result result = handler.Handle();

            if(result.IsFailure) return result.Error;

            Status = HiloStatus.Eliminado;
            
            return Result.Success();
        }

        public void Archivar(Usuario usuario){
            if(usuario is Moderador){
                Archivar();
            }
        }

        public void Archivar(){
            if(Status != HiloStatus.Archivado){
                Status = HiloStatus.Archivado;
            }
        }
        public bool EstaActivo() => Status == HiloStatus.Activo;

        public Result VotarEncuesta(Encuesta encuesta,RespuestaId respuestaId, UsuarioId votante){
            ValidationHandler handler = new HiloDebeEstarActivoValidator(this);

            Result result = handler.Handle();          

            if(result.IsFailure) return result;

            result = encuesta.Votar(votante, respuestaId);

            if(result.IsFailure) return result;

            return Result.Success();
        }

    }


    public class HiloNoPuedeEstarEliminado : ValidationHandler
    {
        private readonly Hilo _hilo;

        public HiloNoPuedeEstarEliminado(Hilo hilo)
        {
            _hilo = hilo;
        }

        public override Result Handle() {
            if(_hilo.Status == Hilo.HiloStatus.Eliminado){
                return new Error("Hilos.HiloYaEliminado");
            }
            return base.Handle();
        }
    }
}