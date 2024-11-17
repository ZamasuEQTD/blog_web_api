using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Baneos
{
    public class Baneo : Entity<BaneoId>
    {
        public UsuarioId ModeradorId { get; private set; }
        public UsuarioId UsuarioBaneadoId { get; private set; }
        public DateTime? Concluye { get; private set; }
        public string? Mensaje { get; private set; }
        public bool Activo(DateTime utcNow) => Concluye is not null && utcNow < Concluye;

        public Baneo(UsuarioId moderadorId, UsuarioId usuarioBaneadoId, DateTime? concluye, string? mensaje)
        {
            this.Id = new BaneoId(Guid.NewGuid());
            this.ModeradorId = moderadorId;
            this.UsuarioBaneadoId = usuarioBaneadoId;
            this.Concluye = concluye;
            this.Mensaje = mensaje;
        }

        public void Eliminar(DateTime now)
        {
            Concluye = now;
        }

       
    }

    public class BaneoId : EntityId
    {
        public BaneoId(Guid id) : base(id) { }
    }
}