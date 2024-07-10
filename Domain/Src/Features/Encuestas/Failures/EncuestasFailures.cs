using SharedKernel;

namespace Domain.Encuestas.Failures
{
    public static class EncuestasFailures
    {
        static public readonly Error RESPUESTA_INEXISTENTE= new Error("Encuestas.YaHaVotadoEnEncuesta");

        static public readonly Error YA_HA_VOTADO = new Error("Encuestas.YaHaVotadoEnEncuesta");
    }
}