
namespace Application.Hilos
{
    public class HiloNoEncontrado : InvalidCommandException
    {
        public HiloNoEncontrado() : base(["Hilo no encontrado"])
        {
        }
    }
}