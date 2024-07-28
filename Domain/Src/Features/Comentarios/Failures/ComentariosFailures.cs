using SharedKernel;

namespace Domain.Comentarios.Failures
{
    static public class ComentariosFailures
    {
        static public readonly Error COMENTARIO_INEXISTENTE = new Error("Comentario.ComentarioInexistente");
        static public readonly Error TAG_UNICO_REQUERIDO = new Error("Comentarios.TagUnicoRequerido");
        static public readonly Error DADOS_REQUERIDOS = new Error("Comentarios.DadosRequeridos");
        static public readonly Error TAG_UNICO_NO_REQUERIDO = new Error("Comentarios.TagUnicoNoEsNecesario");
        static public readonly Error DADOS_NO_REQUERIDOS = new Error("Comentarios.DadosNoRequeridos");

    }
}