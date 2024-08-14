using Domain.Categorias;
using Domain.Comentarios;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Medias;
using Domain.Stickies;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos
{
    public class Hilo : Entity<HiloId>
    {
        public readonly static int CANTIDAD_MAXIMA_DE_DESTACADOS = 5;
        public UsuarioId AutorId { get; private set; }
        public MediaReferenceId Portada { get; private set; }
        public SubcategoriaId Categoria { get; private set; }
        public EncuestaId? Encuesta { get; private set; }
        public Titulo Titulo { get; private set; }
        public Descripcion Descripcion { get; private set; }
        public HiloStatus Status { get; private set; }
        public ConfiguracionDeComentarios Configuracion { get; private set; }
        public bool RecibirNotificaciones { get; private set; }
        public bool Activo => Status == HiloStatus.Activo;
        public bool Eliminado => Status == HiloStatus.Eliminado;
        public bool Archivado => Status == HiloStatus.Archivado;
        public bool EsAutor(UsuarioId usuario) => AutorId == usuario;
        public Hilo(
            Titulo titulo,
            Descripcion descripcion,
            MediaReferenceId portada,
            UsuarioId autor,
            SubcategoriaId subcategoria,
            EncuestaId? encuesta,
            ConfiguracionDeComentarios configuracion
        )
        {
            Id = new HiloId(Guid.NewGuid());
            AutorId = autor;
            Portada = portada;
            Categoria = subcategoria;
            Encuesta = encuesta;
            Titulo = titulo;
            Descripcion = descripcion;
            Configuracion = configuracion;
            Status = HiloStatus.Activo;
        }

        public async Task<Result> Eliminar(
            IHilosRepository hilosRepository,
            DateTime now
        )
        {

            if (Status == HiloStatus.Eliminado) return HilosFailures.YaEliminado;

            Status = HiloStatus.Eliminado;

            Sticky? sticky = await hilosRepository.GetStickyActivo(Id, now);

            if (sticky != null)
            {
                sticky.Eliminar();
            }

            foreach (var denuncia in await hilosRepository.GetDenuncias(Id))
            {
                denuncia.Desestimar();
            }

            return Result.Success();
        }

        public async Task<Result<Voto>> Votar(
            IEncuestasRepository encuestaRepository,
            UsuarioId usuario,
            RespuestaId respuesta
        )
        {
            if (await encuestaRepository.HaVotado(Encuesta!, usuario)) return EncuestaFailures.YaHaVotado;

            if (!await encuestaRepository.ExisteRespuesta(Encuesta!, respuesta)) return EncuestaFailures.RespuestaInexistente;

            return new Voto(
                usuario,
                respuesta
            );
        }

        public async Task<Result<DenunciaDeHilo>> Denunciar(
            IHilosRepository hilosRepository,
            UsuarioId usuarioId
        )
        {
            if (await hilosRepository.HaDenunciado(Id, usuarioId)) return HilosFailures.YaHaDenunciado;

            return new DenunciaDeHilo(usuarioId, Id);
        }

        public async Task<Result<Sticky>> EstablecerSticky(IHilosRepository hilosRepository, DateTime? concluye)
        {
            if (await hilosRepository.TieneStickyActivo(this.Id)) return HilosFailures.YaTieneStickyActivo;

            return new Sticky(
                Id,
                concluye
            );
        }

        public Result CambiarRelacion(RelacionDeHilo relacion, RelacionDeHilo.Acciones accion)
        {
            if (!Activo) return HilosFailures.Inactivo;

            relacion.EjecutarAccion(accion);

            return Result.Success();
        }

        public void Archivar()
        {
            Status = HiloStatus.Archivado;
        }

        public enum HiloStatus
        {
            Activo,
            Eliminado,
            Archivado
        }

    }

    public class HiloId : EntityId
    {
        public HiloId(Guid id) : base(id) { }
    }
}