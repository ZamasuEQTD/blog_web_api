using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
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
        public InformacionDeComentario Informacion { get; private set; }
        public List<DenunciaDeComentario> Denuncias { get; private set; }
        public List<RelacionDeComentario> Relaciones { get; private set; }
        public ComentarioStatus Status { get; private set; }
        public bool RecibirNotificaciones { get; private set; }
        public bool Activo => Status == ComentarioStatus.Activo;
        private Comentario() { }
        public Comentario(HiloId hilo, UsuarioId autor, Texto texto, InformacionDeComentario informacion)
        {
            Id = new(Guid.NewGuid());
            AutorId = autor;
            Hilo = hilo;
            Texto = texto;
            Status = ComentarioStatus.Activo;
            Informacion = informacion;
            Denuncias = [];
            Relaciones = [];
        }

        public enum ComentarioStatus
        {
            Activo,
            Eliminado
        }

        internal void Eliminar()
        {
            foreach (var denuncia in Denuncias)
            {
                denuncia.Desestimar();
            }

            Status = ComentarioStatus.Eliminado;
        }

        public Result Denunciar(Hilo hilo, UsuarioId usuarioId)
        {
            if (!hilo.Activo) return HilosFailures.Inactivo;

            if (HaDenunciado(usuarioId)) return ComentariosFailures.YaHaDenunciado;

            Denuncias.Add(new DenunciaDeComentario(usuarioId, Id, DenunciaDeComentario.RazonDeDenuncia.Otro));

            return Result.Success();
        }

        public bool HaDenunciado(UsuarioId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);

        public Result Ocultar(Hilo hilo, RelacionDeComentario relacion)
        {
            if (!hilo.Activo) return HilosFailures.Inactivo;

            if (!Activo) return ComentariosFailures.Inactivo;

            relacion.Ocultar();

            return Result.Success();
        }

        static public Comentario Create(
            HiloId hilo,
            UsuarioId usuario,
            Texto texto
        )
        {
            var c = new Comentario(
                    hilo,
                    usuario,
                    texto,
                    new InformacionDeComentario(
                        Tag.Create("DSADASDD").Value,
                        null,
                        null
                    )
            );

            return c;
        }
    }

    public class ComentarioId : EntityId
    {
        private ComentarioId() { }
        public ComentarioId(Guid id) : base(id) { }
    }

    public class InformacionDeComentario
    {
        public Tag Tag { get; private set; }
        public TagUnico? TagUnico { get; private set; }
        public Dados? Dados { get; private set; }

        private InformacionDeComentario() { }
        public InformacionDeComentario(Tag tag, TagUnico? tagUnico, Dados? dados)
        {
            Tag = tag;
            TagUnico = tagUnico;
            Dados = dados;
        }
    }
    public interface IComentariosRepository
    {
        void Add(Denuncias.Denuncia denuncia);
        Task<Comentario?> GetComentarioById(ComentarioId id);
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
        public static readonly Error ValorDeDadosInvalidos = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Ya has denunciado el comentario");

    }

}