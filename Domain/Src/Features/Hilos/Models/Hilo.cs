using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Encuestas;
using Domain.Hilos.ValueObjects;
using Domain.Notificaciones;
using Domain.Stickies;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;
using Domain.Features.Medias.Models.ValueObjects;
using Domain.Hilos.DomainEvents;
using Domain.Hilos.Events;

namespace Domain.Hilos
{

    public class Hilo : Entity<HiloId>
    {
        public readonly static int CANTIDAD_MAXIMA_DE_DESTACADOS = 5;
        public bool RecibirNotificaciones { get; private set; }
        public Autor Autor { get; private set; }
        public Titulo Titulo { get; private set; }
        public Descripcion Descripcion { get; private set; }
        public DateTime UltimoBump { get; private set; }
        public HiloStatus Status { get; private set; }
        public ConfiguracionDeComentarios Configuracion { get; private set; }
        public Sticky? Sticky { get; private set; }
        public List<DenunciaDeHilo> Denuncias { get; private set; }
        public List<Comentario> Comentarios { get; private set; } = [];
        public List<ComentarioDestacado> ComentarioDestacados { get; private set; }
        public List<HiloInteraccionNotificacion> Notificaciones { get; private set; } = [];
        public List<HiloInteraccion> Interacciones { get; private set; } = [];
        public UsuarioId AutorId { get; private set; }
        public SubcategoriaId SubcategoriaId { get; private set; }
        public EncuestaId? EncuestaId    { get; private set; }
        public MediaSpoileableId PortadaId { get; private set; }
        public bool Activo => Status == HiloStatus.Activo;
        public bool Eliminado => Status == HiloStatus.Eliminado;
        public bool Archivado => Status == HiloStatus.Archivado;

        private Hilo() { }

        public Hilo(
            Titulo titulo,
            Descripcion descripcion,
            Autor autor,
            UsuarioId autorId,
            MediaSpoileableId portada,
            SubcategoriaId subcategoria,
            EncuestaId? encuesta,
            ConfiguracionDeComentarios configuracion
        )
        {
            Id = new HiloId(Guid.NewGuid());
            Autor = autor;
            AutorId = autorId;
            SubcategoriaId = subcategoria;  
            EncuestaId = encuesta;
            Titulo = titulo;
            PortadaId = portada;
            Descripcion = descripcion;
            RecibirNotificaciones = true;
            UltimoBump = DateTime.UtcNow;
            Configuracion = configuracion;
            Status = HiloStatus.Activo;
            Denuncias = [];
            ComentarioDestacados = [];
        }

        static public Hilo Create(
            Titulo titulo,
            Descripcion descripcion,
            Autor autor,
            UsuarioId autorId,
            MediaSpoileableId portada,
            SubcategoriaId subcategoria,
            EncuestaId? encuesta,
            ConfiguracionDeComentarios configuracion
        ){
            Hilo hilo = new Hilo(
                titulo,
                descripcion,
                autor,
                autorId,
                portada,
                subcategoria,
                encuesta,
                configuracion
            );

            hilo.Raise(new HiloPosteadoDomainEvent(hilo.Id));

            return hilo;
        }

        public Result Eliminar( )
        {
            if (Eliminado) return HilosFailures.YaEliminado;

            if (TieneStickyActivo())
            {
                Sticky = null;
            }

            foreach (var denuncia in Denuncias)
            {
                denuncia.Desestimar();
            }

            Status = HiloStatus.Eliminado;

            Raise(new HiloEliminadoDomainEvent(Id));

            return Result.Success();
        }

        public Result EstablecerSticky( )
        {
            if (TieneStickyActivo( )) return HilosFailures.YaTieneStickyActivo;

            this.Sticky = new Sticky(this.Id);

            return Result.Success();
        }

        public Result EliminarSticky( )
        {
            if (!TieneStickyActivo( )) return HilosFailures.SinStickyActivo;

            this.Sticky = null;

            return Result.Success();
        }

        public Result Denunciar(UsuarioId usuarioId)
        {
            if (HaDenunciado(usuarioId)) return HilosFailures.YaHaDenunciado;

            Denuncias.Add(new DenunciaDeHilo(
                usuarioId,
                Id
            ));

            return Result.Success();
        }

        public Result Seguir(UsuarioId usuarioId)
        {
            if (!Activo) return HilosFailures.Inactivo;

            HiloInteraccion? relacion = Interacciones.FirstOrDefault(i => i.UsuarioId == usuarioId);

            if(relacion is null) {
                relacion = new HiloInteraccion(Id, usuarioId);

                Interacciones.Add(relacion);
            };

            relacion.Seguir();

            return Result.Success();
        }

        public Result Ocultar(UsuarioId usuarioId)
        {
            if (!Activo) return HilosFailures.Inactivo;

            HiloInteraccion? relacion = Interacciones.FirstOrDefault(i => i.UsuarioId == usuarioId);

            if(relacion is null) {
                relacion = new HiloInteraccion(Id, usuarioId);

                Interacciones.Add(relacion);
            };

            relacion.Ocultar();

            return Result.Success();
        }

        public Result PonerEnFavoritos(UsuarioId usuarioId)
        {
            if (!Activo) return HilosFailures.Inactivo;

            HiloInteraccion? relacion = Interacciones.FirstOrDefault(i => i.UsuarioId == usuarioId);

            if(relacion is null) {
                relacion = new HiloInteraccion(Id, usuarioId);

                Interacciones.Add(relacion);
            };

            relacion.PonerEnFavoritos();

            return Result.Success();
        }

        public Result Destacar(UsuarioId usuarioId, Comentario comentario)
        {
            if (!EsAutor(usuarioId)) return HilosFailures.NoEsAutor;

            if (!comentario.Activo) return ComentariosFailures.Inactivo;

            if (EstaDestacado(comentario.Id))
            {
                DejarDeDestacarComentario(comentario.Id);

                return Result.Success();
            }

            if (HaAlcandoMaximaCantidadDeDestacados) return ComentariosFailures.HaAlcanzadoMaximaCantidadDeDestacados;

            DestacarComentario(comentario);

            return Result.Success();
        }

        public Result Comentar(Comentario comentario, DateTime now)
        {
            if (!Activo) return HilosFailures.Inactivo;

            Comentarios.Add(comentario);

            List<string> tags = TagUtils.GetTags(comentario.Texto.Value); 

            if(AutorId != comentario.AutorId)
            {
                Notificaciones.Add(new HiloComentadoNotificacion(AutorId, Id, comentario.Id));
            }

            foreach (var tag in tags)
            {
                Comentario? respondido = Comentarios.FirstOrDefault(c => c.Tag == Tag.Create(tag).Value);

                if (respondido is not null)
                {
                    respondido.AgregarRespuesta(comentario.Id);

                    if(respondido.AutorId != comentario.AutorId)
                    {
                        Notificaciones.Add(new ComentarioRespondidoNotificacion(
                            respondido.AutorId,
                            Id,
                            comentario.Id,
                            respondido.Id
                        ));
                    }
                }
            }

            List<UsuarioId> seguidores = Interacciones.Where(i => i.Seguido).Select(i => i.UsuarioId).ToList();

            foreach (UsuarioId seguidor in seguidores)
            {
                Notificaciones.Add(new HiloSeguidoNotificacion(seguidor, Id, comentario.Id));
            }


            this.UltimoBump = now;

            Raise(new HiloComentadoDomainEvent(Id, comentario.Id));

            return Result.Success();
        }

        public void ModificarSubcategoria(SubcategoriaId subcategoriaId)
        {
            SubcategoriaId = subcategoriaId;
        }

        private void DestacarComentario(Comentario comentario) => ComentarioDestacados.Add(new(comentario.Id, Id));
        public void DejarDeDestacarComentario(ComentarioId comentarioId) => ComentarioDestacados = ComentarioDestacados.Where(c => c.Id == comentarioId).ToList();
        public bool HaDenunciado(UsuarioId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
        bool HaAlcandoMaximaCantidadDeDestacados => ComentarioDestacados.Count == 5;
        public bool EstaDestacado(ComentarioId comentarioId) => ComentarioDestacados.Any(c => c.ComentarioId == comentarioId);
        public bool EsAutor(UsuarioId usuario) => AutorId == usuario;
        public bool TieneStickyActivo( ) => Sticky is not null;
    }

    public class HiloId : EntityId
    {
        private HiloId() { }
        public HiloId(Guid id) : base(id) { }
    }
}