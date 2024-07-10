using Domain.Comentarios.Failures;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Failures;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios {
    public class Comentario : Entity<ComentarioId> {
        public UsuarioId AutorId { get;private set; }
        public Hilo.HiloId HiloId { get; private set;}
        public InformacionComentario Informacion { get; private set; }
        public ComentarioStatus Status { get; private set; }	
        public string Texto { get; private set; }
        public Comentario(
            ComentarioId id,
            UsuarioId autorId,
            Hilo.HiloId hiloId,
            InformacionComentario informacion,
            string texto
        ) : base(id) {
            Informacion = informacion;
            Texto = texto;
            HiloId = hiloId;
            AutorId = autorId;
            Status = ComentarioStatus.Activo;
        }

        static public Result<Comentario> Create(
            ComentarioId id,
            UsuarioId autor,
            Hilo hilo,
            InformacionComentario informacion,
            string texto
        ){
            ValidationHandler handler = new HiloDebeEstarActivoValidator(hilo);
            handler
            .SetNext(new NoPuedeTaguearMasComentariosQueLaPermitida(texto))
            .SetNext(new DadosRequeridosValidator(hilo.Configuracion, informacion.Dados))
            .SetNext(new TagUnicoRequeridoValidator(hilo.Configuracion, informacion.TagUnico));
            
            Result result = handler.Handle();

            if(result.IsFailure){
                return result.Error;
            }

            return new Comentario(
                id,
                autor,
                hilo.Id,
                informacion,
                texto
            );
        }

        public enum ComentarioStatus
        {
            Activo,
            Eliminado
        }
    }

    public class InformacionComentario {
        public Tag Tag { get; private set; }
        public Dados? Dados {get;private set;}
        public TagUnico? TagUnico{ get;private set; }

        public InformacionComentario(Tag tag, Dados? dados,TagUnico? tagUnico)
        {
            this.Tag = tag;
            this.Dados = dados;
            this.TagUnico = tagUnico;
        }
    }
    
    public class DadosRequeridosValidator :ValidationHandler{
        private readonly ConfiguracionDeComentarios configuracion;
        private readonly Dados? dados;
        public DadosRequeridosValidator(ConfiguracionDeComentarios configuracion, Dados? dados)
        {
            this.configuracion = configuracion;
            this.dados = dados;
        }

        public override Result Handle()
        {
            if(dados is null && configuracion.Dados){
                return ComentariosFailures.DADOS_REQUERIDOS;
            }
            if(dados is not null && !configuracion.Dados){
                return ComentariosFailures.DADOS_NO_REQUERIDOS;
            }
            return base.Handle();
        }
    }

    public class TagUnicoRequeridoValidator :ValidationHandler{
        private readonly ConfiguracionDeComentarios configuracion;
        private readonly TagUnico? tag;
        public TagUnicoRequeridoValidator(ConfiguracionDeComentarios configuracion, TagUnico? tag)
        {
            this.configuracion = configuracion;
            this.tag = tag;
        }

        public override Result Handle()
        {
            if(tag is null && configuracion.IdUnicoActivado){
                return ComentariosFailures.TAG_UNICO_REQUERIDO;
            } else if(tag is not null && !configuracion.IdUnicoActivado){
                return ComentariosFailures.TAG_UNICO_NO_REQUERIDO;
            }
            return base.Handle();
        }
    }

    public class HiloDebeEstarActivoValidator : ValidationHandler{
        private readonly Hilo _hilo;
        public HiloDebeEstarActivoValidator(Hilo hilo)
        {
            _hilo = hilo;
        }

        public override Result Handle() {
            if(!_hilo.EstaActivo()){
                return HilosFailures.HILO_INACTIVO;
            }

            return base.Handle();
        }
    }


    public class NoPuedeTaguearMasComentariosQueLaPermitida : ValidationHandler {
        private readonly string _texto;

        public NoPuedeTaguearMasComentariosQueLaPermitida(string texto)
        {
            _texto = texto;
        }

        public override Result Handle()
        {
            if(TagUtils.CantidadDeTags(_texto) > 5){
                return new Error("Comentarios.NoPuedesTagguearMasDeCincoComentarios");
            }
            return base.Handle();
        }
    }
}