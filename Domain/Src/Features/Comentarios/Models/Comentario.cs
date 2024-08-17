using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios
{
    public class Comentario : Entity<ComentarioId>
    {
        public UsuarioId AutorId { get; private set; }
        public HiloId Hilo { get; private set; }
        public Texto Texto { get; private set; }
        public ComentarioStatus Status { get; private set; }
        public bool Destacado { get; private set; }
        public bool RecibirNotificaciones { get; private set; }
        public bool Activo => Status == ComentarioStatus.Activo;
        private Comentario() { }
        public Comentario(HiloId hilo, UsuarioId autor, Texto texto)
        {
            Id = new(Guid.NewGuid());
            AutorId = autor;
            Hilo = hilo;
            Texto = texto;
        }

        public enum ComentarioStatus
        {
            Activo,
            Eliminado
        }

        public Result Eliminar(Hilo hilo)
        {
            if (!hilo.Activo) return HilosFailures.Inactivo;

            if (Destacado)
            {
                Destacado = false;
            }

            Status = ComentarioStatus.Eliminado;

            return Result.Success();
        }

        public async Task<Result<DenunciaDeComentario>> Denunciar(IComentariosRepository comentariosRepository, UsuarioId usuario)
        {
            if (await comentariosRepository.HaDenunciado(Id, usuario)) return ComentariosFailures.YaHaDenunciado;

            return new DenunciaDeComentario(
                usuario,
                this.Id,
                0
            );
        }

        public async Task<Result> Destacar(
            Hilo hilo,
            UsuarioId usuario,
            IComentariosRepository comentariosRepository)
        {
            if (!hilo.Activo) return HilosFailures.Inactivo;

            if (hilo.EsAutor(usuario)) return HilosFailures.NoEsAutor;

            if (!Activo) return ComentariosFailures.Inactivo;

            if (!Destacado)
            {
                if (await comentariosRepository.CantidadDeDestacados(this.Hilo) > 5) return ComentariosFailures.HaAlcanzadoMaximaCantidadDeDestacados;

                Destacado = true;
            }
            else
            {
                Destacado = false;
            }

            return Result.Success();
        }

        public Result Ocultar(Hilo hilo, RelacionDeComentario relacion)
        {
            if (!hilo.Activo) return HilosFailures.Inactivo;

            if (!Activo) return ComentariosFailures.Inactivo;

            relacion.Ocultar();

            return Result.Success();
        }
    }

    public class ComentarioId : EntityId
    {
        private ComentarioId() { }
        public ComentarioId(Guid id) : base(id) { }
    }

    public interface IComentariosRepository
    {
        Task<Comentario?> GetComentarioById(ComentarioId id);
        Task<bool> HaDenunciado(ComentarioId id, UsuarioId usuarioId);
        void Add(Denuncias.Denuncia denuncia);
        Task<int> CantidadDeDestacados(HiloId hilo);
        Task<RelacionDeComentario?> GetRelacionDeComentario(UsuarioId usuario, ComentarioId comentario);
    }

    static public class EncuestaFailures
    {
        public static readonly Error YaHaVotado = new Error("Comentarios.YaHaDenunciado", "Ya has denunciado el comentario");
        public static readonly Error RespuestaInexistente = new Error("Comentarios.YaHaDenunciado", "Ya has denunciado el comentario");

    }


    static public class HilosFailures
    {
        public static readonly Error SinPortada = new Error("Hilos.SinPortada", "El hilo no tiene portada");
        public static readonly Error YaEliminado = new Error("Hilos.YaEliminado", "El hilo ya ha sido eliminado");
        public static readonly Error Inactivo = new Error("Hilos.Inactivo", "El hilo está inactivo");
        public static readonly Error NoEsAutor = new Error("Hilos.NoEsAutor", "No eres el autor del hilo");
        public static readonly Error YaHaDenunciado = new Error("Hilos.YaHaDenunciado", "Ya has denunciado este hilo");
        public static readonly Error YaTieneStickyActivo = new Error("Hilos.YaTieneStickyActivo", "Ya tiene un sticky activo");
        public static readonly Error SinStickyActivo = new Error("Hilos.SinStickyActivo", "No tienes un sticky activo");
        public static readonly Error NoEncontrado = new Error("Hilos.NoEncontrado", "El hilo no fue encontrado");
        public static readonly Error LongitudDeTituloInvalida = new Error("Hilos.LongitudDeTituloInvalida", "La longitud del título es inválida");
        public static readonly Error LongitudDeDescripcionInvalida = new Error("Hilos.LongitudDeDescripcionInvalida", "La longitud de la descripción es inválida");
    }

    static public class ComentariosFailures
    {
        public static readonly Error NoEncontrado = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");
        public static readonly Error Inactivo = new Error("Comentarios.YaHaDenunciado", "Ya has denunciado el comentario");
        public static readonly Error YaHaDenunciado = new Error("Comentarios.YaHaDenunciado", "Ya has denunciado el comentario");
        public static readonly Error HaAlcanzadoMaximaCantidadDeDestacados = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");
        public static readonly Error TagInvalido = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");
        public static readonly Error TagUnicoInvalido = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");
        public static readonly Error LongitudDeTextoInvalido = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");
        public static readonly Error MaximaCantidadDeTaggueosSuperados = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");

    }

}