using SharedKernel;

namespace Domain.Hilos.Failures
{
    static public class HilosFailures
    {

        static readonly public Error HILO_YA_ELIMINADO = new Error("Hilos.HiloYaEliminado");
        static readonly public Error HILO_INEXISTENTE = new Error("Hilos.HiloInexistente");
        static readonly public Error HILO_INACTIVO = new Error("Hilos.HiloInexistente");
    }
}