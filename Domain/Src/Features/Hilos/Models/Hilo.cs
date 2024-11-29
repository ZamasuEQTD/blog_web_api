using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Encuestas;
using Domain.Media;
using Domain.Hilos.Events;
using Domain.Hilos.ValueObjects;
using Domain.Media;
using Domain.Notificaciones;
using Domain.Stickies;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;
using static Domain.Hilos.Hilo;
using Domain.Media.ValueObjects;

namespace Domain.Hilos
{

    public class Hilo : Entity<HiloId>
    {
        public readonly static int CANTIDAD_MAXIMA_DE_DESTACADOS = 5;
        public bool RecibirNotificaciones { get; private set; }
        public Titulo Titulo { get; private set; }
        public Descripcion Descripcion { get; private set; }
        public DateTime UltimoBump { get; private set; }
        public HiloStatus Status { get; private set; }
        public ConfiguracionDeComentarios Configuracion { get; private set; }
        public Sticky? Sticky { get; private set; }
        public List<DenunciaDeHilo> Denuncias { get; private set; }
        public List<Comentario> Comentarios { get; private set; } = [];
        public List<ComentarioDestacado> ComentarioDestacados { get; private set; }
        public UsuarioId AutorId { get; private set; }
        public SubcategoriaId Categoria { get; private set; }
        public EncuestaId? Encuesta { get; private set; }
        public MediaReferenceId PortadaId { get; private set; }
        public bool Activo => Status == HiloStatus.Activo;
        public bool Eliminado => Status == HiloStatus.Eliminado;
        public bool Archivado => Status == HiloStatus.Archivado;

        private Hilo() { }

        public Hilo(
            Titulo titulo,
            Descripcion descripcion,
            UsuarioId autor,
            MediaReferenceId portada,
            SubcategoriaId subcategoria,
            EncuestaId? encuesta,
            ConfiguracionDeComentarios configuracion
        )
        {
            Id = new HiloId(Guid.NewGuid());
            AutorId = autor;
            Categoria = subcategoria;
            Encuesta = encuesta;
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

        public Result Eliminar( )
        {
            if (Eliminado) return HilosFailures.YaEliminado;

            if (TieneStickyActivo( ))
            {
                Sticky = null;
            }

            foreach (var denuncia in Denuncias)
            {
                denuncia.Desestimar();
            }

            Status = HiloStatus.Eliminado;

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

        public Result Seguir(RelacionDeHilo relacion)
        {
            if (!Activo) return HilosFailures.Inactivo;

            relacion.Seguir();

            return Result.Success();
        }

        public Result Ocultar(RelacionDeHilo relacion)
        {
            if (!Activo) return HilosFailures.Inactivo;

            relacion.Ocultar();

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

        public void Comentar(Comentario comentario, DateTime now)
        {
            Comentarios.Add(comentario);

            List<string> tags = TagUtils.GetTags(comentario.Texto.Value); 

            foreach (var tag in tags)
            {
                Comentario? respondido = Comentarios.FirstOrDefault(c => c.Tag == Tag.Create(tag).Value);

                if (respondido is not null)
                {
                    respondido.AgregarRespuesta(comentario.Id);
                }
            }
            this.UltimoBump = now;
        }

        public Result Eliminar(Comentario comentario)
        {
            if (!Activo) return HilosFailures.Inactivo;

            if (EstaDestacado(comentario.Id))
            {
                DejarDeDestacarComentario(comentario.Id);
            }

            comentario.Eliminar();

            return Result.Success();
        }

        private void DestacarComentario(Comentario comentario) => ComentarioDestacados.Add(new(comentario.Id, Id));
        private void DejarDeDestacarComentario(ComentarioId comentarioId) => ComentarioDestacados = ComentarioDestacados.Where(c => c.Id == comentarioId).ToList();
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

    public class Autor {
        public string Nombre {get; set;}
        public string Rango {get; set;}
        public string RangoCorto {get; set;}
    }
}