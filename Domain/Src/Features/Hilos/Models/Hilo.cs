using Domain.Comentarios;
using SharedKernel.Abstractions;
using Domain.Stickies;
using Domain.Hilos.ValueObjects;
using Domain.Hilos.Rules;
using Domain.Categorias;
using Domain.Usuarios;
using Domain.Encuestas;
using Domain.Denuncias.Rules;
using Domain.Comentarios.Rules;
using Domain.Usuarios.Rules;

namespace Domain.Hilos {
    public class Hilo : Entity<HiloId> {
        public readonly static int CANTIDAD_MAXIMA_DE_DESTACADOS = 5;
        public UsuarioId AutorId { get; private  set; }
        public SubcategoriaId Categoria {get;private set;}
        public List<Sticky> HistorialStickies {get;private set;}
        public List<Comentario> Comentarios {get;private set;}
        public List<Denuncia> Denuncias {get;private set;}
        public List<Interaccion> Interacciones {get; private set;}
        public List<Notificacion> Notificaciones {get; private set;}
        public EncuestaId? Encuesta {get;private set;}
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public HiloStatus Status { get; private set; }
        public ConfiguracionDeComentarios Configuracion {get;private set;}
        public bool RecibirNotificaciones {get;private set;}
        public bool Activo => Status == HiloStatus.Activo;
        public bool Eliminado => Status == HiloStatus.Eliminado;
        public bool Archivado => Status == HiloStatus.Archivado;
        public int CantidadDeDestacados => Comentarios.Count(c=> c.Destacado);
        public bool TieneStickyActivo(DateTime utcNow) => HistorialStickies.Any(s=> s.Activo(utcNow));

        private Hilo(){}

        public Hilo(
            string titulo,
            string descripcion,
            SubcategoriaId categoria,
            Usuario usuario,
            EncuestaId? encuesta,
            ConfiguracionDeComentarios configuracion
        ){
            this.Id = new HiloId(Guid.NewGuid());
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.AutorId = usuario.Id;
            this.Encuesta = encuesta;
            this.Categoria = categoria;
            this.Configuracion = configuracion;
            this.Status = HiloStatus.Activo;
            this.Comentarios = [];
            this.Denuncias = [];
            this.Interacciones = [];
            this.HistorialStickies = [];
        }
        public void Comentar(
            Comentario comentario
        )
        {
            this.CheckRule(new HiloDebeEstarActivoRule(Status));
            this.CheckRule(new ValidarDadosEnHiloRule(comentario.Informacion, Configuracion));
            this.CheckRule(new ValidarTagUnicoEnHiloRule(comentario.Informacion, Configuracion));

            this.Comentarios.Add(comentario);
        }

        public void Eliminar(DateTime utcNow){
            if(TieneStickyActivo(utcNow)){
                EliminarSticky(utcNow);
            }

            Status = HiloStatus.Eliminado;
            DesestimarDenuncias();
        }

        public void Eliminar(ComentarioId id){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));

            Comentario comentario = GetComentario(id);
            
            comentario.Eliminar();
        }
        
        public void Activar(){
            Status = HiloStatus.Activo;
        }

        private void DesestimarDenuncias(){
            foreach (var d in Denuncias) {
                d.Desestimar();
            }
        }

        public void Archivar(){
            Status = HiloStatus.Archivado;
        }

        public void DenunciarComentario(ComentarioId id, UsuarioId usuarioId){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));

            Comentario comentario = GetComentario(id);

            comentario.Denunciar(usuarioId);
        }

        public void Denunciar(UsuarioId usuarioId){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));
            this.CheckRule(new SoloPuedeDenunciarUnaVezRule(Denuncias, usuarioId));
            
            Denuncias.Add(new Denuncia(
                usuarioId,
                Id
            ));
        }

        public void ModificarPreferenciaNotificacionesDeComentario(ComentarioId id, UsuarioId usuarioId) {
            this.CheckRule(new HiloDebeEstarActivoRule(Status));

            Comentario comentario = GetComentario(id);
        
            comentario.ModificarPreferenciaNotificaciones(usuarioId);
        }    

        public void DestacarComentario(ComentarioId id, UsuarioId usuarioId){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));
            this.CheckRule(new SoloAutorPuedeRealizarEstaAccionRule(usuarioId, AutorId));
            this.CheckRule(new NoDebeAlcanzarMaximaCantidadDeDestacadosRule(Comentarios));

            Comentario comentario = GetComentario(id);

            comentario.Destacar();
        }

        public void EstablecerSticky(DateTime terminaEn,DateTime utcNow) {
            this.CheckRule(new HiloDebeEstarActivoRule(Status));
            this.CheckRule(new NoDebeTenerStickyActivoRule(utcNow,HistorialStickies));
            this.HistorialStickies.Add(
                new Sticky(
                    Id,
                    terminaEn
                )
            );
        }

        public void EliminarSticky(DateTime utcNow){
            this.CheckRule(new DebeTenerStickyActivoRule(utcNow, HistorialStickies));

            Sticky sticky = HistorialStickies.Single(s=> s.Activo(utcNow));

            sticky.Eliminar();
        }

        public void Votar(Encuesta encuesta, UsuarioId votanteId, RespuestaId respuestaId){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));

            encuesta.Votar(
                votanteId,
                respuestaId
            );
        } 
        
        public void DestacarComentario(ComentarioId id){
            this.CheckRule(new ComentarioDebeExistirRule(id, this.Comentarios));

            Comentario comentario = GetComentario(id);

            comentario.Destacar();
        }

        public void CambiarInteraccionDeHilo(Interaccion.Acciones accion, UsuarioId usuarioId){
            Interaccion? interaccion = Interacciones.SingleOrDefault(i=> i.UsuarioId == usuarioId);

            if(interaccion is null){
                interaccion = new Interaccion(
                    this.Id,
                    usuarioId
                );

                Interacciones.Add(interaccion);
            }

            interaccion.EjecutarAccion(accion);
        }

        public void OcultarComentario(ComentarioId id, UsuarioId usuarioId){
            this.CheckRule(new HiloDebeEstarActivoRule(Status));
            this.CheckRule(new ComentarioDebeExistirRule(id, this.Comentarios));
            
            Comentario comentario = Comentarios.Single( c => c.Id == id);

            comentario.Ocultar(usuarioId);
        }
        public void CambiarNotificaciones(UsuarioId usuarioId){
            this.CheckRule(new SoloAutorPuedeRealizarEstaAccionRule(usuarioId,this.AutorId));
            
            RecibirNotificaciones = !RecibirNotificaciones;
        }


        private Comentario GetComentario(ComentarioId id) => Comentarios.Single(c=> c.Id == id);

        public enum HiloStatus {
            Activo,
            Eliminado,
            Archivado
        }
    }

    public class HiloId : EntityId
    {
        public HiloId(Guid id):base(id){}
    }

}