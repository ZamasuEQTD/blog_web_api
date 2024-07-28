using Domain.Comentarios;
using Domain.Comentarios.Validators;
using Domain.Encuestas;
using Domain.Hilos.Failures;
using Domain.Hilos.Validators;
using Domain.Media;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos {
    public class Hilo : Entity <HiloId> {
        public readonly static int CANTIDAD_MAXIMA_DE_DESTACADOS = 5;
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public HiloStatus Status {get; private set;}     
        public UsuarioId AutorId {get; private set; }
        public MediaReferenceId PortadaId { get; private set; }
        public ConfiguracionDeComentarios Configuracion {get;private set;}
        public EncuestaId? EncuestaId {get; private set; }
        public List<Comentario> Comentarios {get; private set;}
        public bool HaAlcanzadoLimiteDeDestacados(int destacados) => destacados  == CANTIDAD_MAXIMA_DE_DESTACADOS;
        public bool EstaActivo => Status == HiloStatus.Activo;
        public bool EstaArchivado => Status == HiloStatus.Archivado;
        public bool EstaEliminado => Status == HiloStatus.Eliminado;
        public bool EsAutor(UsuarioId id) => AutorId == id;

        private Hilo(){}
        private Hilo(
            HiloId id,
            string titulo,
            string descripcion,
            UsuarioId autorId,
            MediaReferenceId portadaId,
            ConfiguracionDeComentarios configuracion,
            EncuestaId? encuestaId
        ) : base(id){
            Titulo = titulo;
            Descripcion = descripcion;
            PortadaId = portadaId;
            AutorId = autorId;
            EncuestaId = encuestaId;
            Configuracion = configuracion;
            Comentarios = [];
            Status = HiloStatus.Activo;
        }

        static public Hilo Create(
            HiloId id,
            UsuarioId autor,
            MediaReferenceId portadaId,
            ConfiguracionDeComentarios configuracion,
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
                configuracion,
                encuestaId
            );
        }

        public Result<Comentario> Comentar(
            UsuarioId autorId,
            InformacionComentario informacion,
            string texto
        ){  
            ValidationHandler handler = new HiloDebeEstarActivoValidator(this);

            handler
            .SetNext(new DadosRequeridosValidator(Configuracion, informacion.Dados))
            .SetNext(new TagUnicoRequeridoValidator(Configuracion, informacion.TagUnico));
            
            Result result = handler.Handle();

            if(result.IsFailure) return result.Error;

            return Comentario.Create(
                new (Guid.NewGuid()),
                autorId,
                this,
                informacion,
                texto
            );
        }

        public Result Eliminar() {
            ValidationHandler handler = new HiloNoPuedeEstarEliminado(this);
            
            Result result = handler.Handle();

            if(result.IsFailure) return result.Error;

            Status = HiloStatus.Eliminado;
            
            return Result.Success();
        }

        public Result VotarEncuesta(Encuesta encuesta,RespuestaId respuestaId, UsuarioId votante){
            ValidationHandler handler = new HiloDebeEstarActivoValidator(this);

            Result result = handler.Handle();          

            if(result.IsFailure) return result;

            result = encuesta.Votar(votante, respuestaId);

            if(result.IsFailure) return result;

            return Result.Success();
        }

        public enum HiloStatus {
            Activo,
            Eliminado,
            Archivado
        }
    }

    public class HiloNoPuedeEstarEliminado : ValidationHandler {
        private readonly Hilo _hilo;

        public HiloNoPuedeEstarEliminado(Hilo hilo)
        {
            _hilo = hilo;
        }

        public override Result Handle() {
            if(_hilo.Status == Hilo.HiloStatus.Eliminado){
                return HilosFailures.HILO_YA_ELIMINADO;
            }
            return base.Handle();
        }
    }
    public class HiloId : EntityId
    {
        public HiloId(Guid id):base(id){}
    }
}