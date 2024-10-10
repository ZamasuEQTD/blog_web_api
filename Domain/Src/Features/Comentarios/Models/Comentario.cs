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
        public Tag Tag { get; private set; }
        public TagUnico? TagUnico { get; private set; }
        public Dados? Dados { get; private set; }
        public Colores Color { get; private set; }
        public List<DenunciaDeComentario> Denuncias { get; private set; }
        public List<RelacionDeComentario> Relaciones { get; private set; }
        public ComentarioStatus Status { get; private set; }
        public bool RecibirNotificaciones { get; private set; }
        public bool Activo => Status == ComentarioStatus.Activo;
        private Comentario() { }
        public Comentario(HiloId hilo, UsuarioId autor, Texto texto, Colores color, InformacionDeComentario informacion)
        {
            Id = new(Guid.NewGuid());
            AutorId = autor;
            Hilo = hilo;
            Texto = texto;
            Status = ComentarioStatus.Activo;
            Tag = informacion.Tag;
            TagUnico = informacion.TagUnico;
            Dados = informacion.Dados;
            Color = color;
            Denuncias = [];
            Relaciones = [];
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
        Task<Comentario?> GetComentarioByTag(HiloId hiloId, Tag tag);

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
        public static readonly Error NoEncontrado = new Error("Comentarios.NoEncontrado", "El comentario no fue encontrado.");
        public static readonly Error Inactivo = new Error("Comentarios.Inactivo", "El comentario está inactivo.");
        public static readonly Error YaHaDenunciado = new Error("Comentarios.YaHaDenunciado", "Ya has denunciado este comentario.");
        public static readonly Error HaAlcanzadoMaximaCantidadDeDestacados = new Error("Comentarios.MaximaCantidadDeDestacadosAlcanzada", "Has alcanzado la máxima cantidad de comentarios destacados.");
        public static readonly Error TagInvalido = new Error("Comentarios.TagInvalido", "El tag proporcionado es inválido.");
        public static readonly Error TagUnicoInvalido = new Error("Comentarios.TagUnicoInvalido", "El tag único proporcionado es inválido.");
        public static readonly Error LongitudDeTextoInvalido = new Error("Comentarios.LongitudDeTextoInvalido", "La longitud del texto del comentario es inválida.");
        public static readonly Error MaximaCantidadDeTaggueosSuperados = new Error("Comentarios.MaximaCantidadDeTaggueosSuperados", "Se ha superado la máxima cantidad de taggueos permitidos.");
        public static readonly Error ValorDeDadosInvalidos = new Error("Comentarios.ValorDeDadosInvalidos", "El valor de los dados proporcionado es inválido.");
    }
}