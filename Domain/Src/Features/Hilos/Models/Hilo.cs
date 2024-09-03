using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Encuestas;
using Domain.Hilos.ValueObjects;
using Domain.Stickies;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;
using static Domain.Hilos.Hilo;

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
        public List<ComentarioDestacado> ComentarioDestacados { get; private set; }
        public UsuarioId AutorId { get; private set; }
        public SubcategoriaId Categoria { get; private set; }
        public EncuestaId? Encuesta { get; private set; }
        public bool Activo => Status == HiloStatus.Activo;
        public bool Eliminado => Status == HiloStatus.Eliminado;
        public bool Archivado => Status == HiloStatus.Archivado;

        private Hilo() { }

        public Hilo(
            Titulo titulo,
            Descripcion descripcion,
            UsuarioId autor,
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
            Descripcion = descripcion;
            RecibirNotificaciones = true;
            UltimoBump = DateTime.UtcNow;
            Configuracion = configuracion;
            Status = HiloStatus.Activo;
            Denuncias = [];
            ComentarioDestacados = [];
        }

        public Result<Comentario> Comentar(
            Texto texto,
            UsuarioId usuarioId
        )
        {
            if (!Activo) return HilosFailures.Inactivo;

            var c = new Comentario(
                Id,
                usuarioId,
                texto,
                new InformacionDeComentario(
                    TagsService.GenerarTag(),
                    Configuracion.IdUnicoActivado ? TagsService.GenerarTagUnico(Id, usuarioId) : null,
                    Configuracion.Dados ? DadosService.Generar() : null
                )
            );

            return c;
        }

        public Result Eliminar(DateTime now)
        {
            if (Eliminado) return HilosFailures.YaEliminado;

            if (TieneStickyActivo(now))
            {
                Sticky!.Eliminar(now);
            }

            foreach (var denuncia in Denuncias)
            {
                denuncia.Desestimar();
            }

            Status = HiloStatus.Eliminado;

            return Result.Success();
        }

        public Result EstablecerSticky(DateTime now)
        {
            if (TieneStickyActivo(now)) return HilosFailures.YaTieneStickyActivo;

            this.Sticky = new Sticky(this.Id, null);

            return Result.Success();
        }

        public Result EliminarSticky(DateTime now)
        {
            if (!TieneStickyActivo(now)) return HilosFailures.SinStickyActivo;

            Sticky!.Eliminar(now);

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
        private bool HaDenunciado(UsuarioId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
        bool HaAlcandoMaximaCantidadDeDestacados => ComentarioDestacados.Count == 5;
        public bool EstaDestacado(ComentarioId comentarioId) => ComentarioDestacados.Any(c => c.ComentarioId == comentarioId);
        public bool EsAutor(UsuarioId usuario) => AutorId == usuario;
        public bool TieneStickyActivo(DateTime now) => Sticky is not null && !Sticky.Conluido(now);
        public enum HiloStatus
        {
            Activo,
            Eliminado,
            Archivado
        }
    }

    public class HiloId : EntityId
    {
        private HiloId() { }
        public HiloId(Guid id) : base(id) { }
    }


}