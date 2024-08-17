using Domain.Denuncias;
using Domain.Usuarios;

namespace Domain.Hilos
{

    public class DenunciaDeHilo : Denuncia
    {
        public HiloId HiloId { get; private set; }
        public RazonDeDenuncia Razon { get; private set; }
        private DenunciaDeHilo() : base() { }
        public DenunciaDeHilo(UsuarioId autorId, HiloId hiloId) : base(autorId)
        {
            this.HiloId = hiloId;
            this.Razon = RazonDeDenuncia.Otro;
        }

        public enum RazonDeDenuncia
        {
            Otro
        }
    }
}