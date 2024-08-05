
using Domain.Comentarios.Rules;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Usuarios;
using Domain.Usuarios.Rules;
using SharedKernel.Abstractions;

namespace Domain.Comentarios {
    public class Comentario : Entity<ComentarioId> {
        public UsuarioId AutorId {get; private set; }
        public Usuario Autor {get; private set; }
        public HiloId Hilo { get; private set; }
        public InformacionComentario Informacion { get; private set; }
        public ComentarioStatus Status { get; private set; }	
        public List<Interaccion> Interacciones { get; private set; }
        public List<Denuncia> Denuncias { get; private set; }
        public string Texto { get; private set; }
        public bool Destacado {get; private set; }
        public bool RecibirNotificaciones {get; private set; }
        public bool Activo => Status == ComentarioStatus.Activo;
        public HashSet<Tag> Taggueos => TagUtils.GetTagsUnicos(Texto);
        private Comentario(){}
        
        public Comentario(
            string texto,
            HiloId hilo,
            UsuarioId autorId,
            InformacionComentario informacion
        ){
            this.Id = new(Guid.NewGuid());
            this.Texto = texto;
            this.Hilo = hilo;
            this.AutorId = autorId;
            this.Informacion = informacion;
            this.Interacciones = [];         
            this.Status = ComentarioStatus.Activo;
            this.Destacado = false;
            this.RecibirNotificaciones = true;
        }

        public void Eliminar(){
            if(Destacado) {
                Destacar();
            }
            Status = ComentarioStatus.Eliminado;
        }
        
        internal void Denunciar(UsuarioId usuarioId){
            Denuncia denuncia = new Denuncia(
                usuarioId,
                Id,
                Denuncia.RazonDeDenuncia.Otro
            );

            this.Denuncias.Add(denuncia);
        }

        internal void Destacar(){
            this.CheckRule(new ComentarioDebeEstarActivoRule(Status));
            Destacado = !Destacado;
        }

        public void ModificarPreferenciaNotificaciones(UsuarioId usuario){
            this.CheckRule(new ComentarioDebeEstarActivoRule(Status));
            this.CheckRule(new SoloAutorPuedeRealizarEstaAccionRule(usuario, this.AutorId));
            RecibirNotificaciones = !RecibirNotificaciones;
        }

        public void Ocultar(UsuarioId usuarioId){
            Interaccion? interaccion = Interacciones.FirstOrDefault(i=> i.UsuarioId == usuarioId);
            
            if(interaccion is  null) {
                interaccion = new Interaccion(this.Id, usuarioId);
            
                Interacciones.Add(interaccion);
            }

            interaccion.Ocultar();
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

        private InformacionComentario(){}

        public InformacionComentario(Tag tag, Dados? dados,TagUnico? tagUnico)
        {
            this.Tag = tag;
            this.Dados = dados;
            this.TagUnico = tagUnico;
        }
    }

    public class ComentarioId : EntityId {
        private ComentarioId (){}
        public ComentarioId(Guid id) : base(id) { }
    }

    public abstract class Notificacion : Entity<NotificacionId>  {
        public UsuarioId NotificadoId { get; private set;}
        public NotificacionStatus Status { get; private set;}

        protected Notificacion(UsuarioId usuarioId){ 
            this.Id = new(Guid.NewGuid());
            this.NotificadoId = usuarioId;
            this.Status = NotificacionStatus.SinLeer;
        }

        public void Leer(){
            this.Status = NotificacionStatus.Leida;
        }

        public enum NotificacionStatus {
            Leida,
            SinLeer
        }
    }


    public class HiloSeguidoComentado : Notificacion
    {
        public HiloSeguidoComentado(UsuarioId usuarioId) : base(usuarioId)
        {
        }
    }

    public class HiloComentadoNotificacion : Notificacion
    {
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }
        public HiloComentadoNotificacion(UsuarioId usuarioId, HiloId hiloId, ComentarioId comentarioId) : base(usuarioId)
        {
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }
    }

    public class ComentarioRespondidoNotificacion : Notificacion
    {
        public ComentarioId ComentarioId { get; private set; }
        public ComentarioId RespuestaId { get; private set; }

        public ComentarioRespondidoNotificacion(UsuarioId usuarioId, ComentarioId comentarioId, ComentarioId respuestaId) : base(usuarioId)
        {
            ComentarioId = comentarioId;
            RespuestaId = respuestaId;
        }

    }

    public class NotificacionId : EntityId {
        public NotificacionId(Guid id) : base(id) {}
    }
}