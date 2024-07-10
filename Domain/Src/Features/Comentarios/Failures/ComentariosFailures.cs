using SharedKernel;

namespace Domain.Comentarios.Failures
{
    static public class ComentariosFailures
    {
        static public readonly Error COMENTARIO_INEXISTENTE = new Error("");
        static public readonly Error TAG_UNICO_REQUERIDO = new Error("");
        static public readonly Error DADOS_REQUERIDOS = new Error("");
        static public readonly Error TAG_UNICO_NO_REQUERIDO = new Error("");
        static public readonly Error DADOS_NO_REQUERIDOS = new Error("");

    }
}