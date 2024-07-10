using Domain.Usuarios;
using SharedKernel;

namespace Domain.Hilos.Abstractions
{
    public interface IHilosManager
    {
        public Result EliminarHilo(Anonimo anonimo);
        public Result EliminarHilo(Moderador moderador);
    }

    public class HilosManager : IHilosManager {
        private readonly Hilo _hilo;

        public HilosManager(Hilo hilo)
        {
            _hilo = hilo;
        }

        public Result EliminarHilo(Anonimo anonimo) {
            return new Error("NoPuedesHacerEsto");
        }

        public Result EliminarHilo(Moderador moderador){
            return _hilo.Eliminar();
        }
    }
}