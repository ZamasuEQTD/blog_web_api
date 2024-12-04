using Domain.Comentarios;
using Domain.Hilos;
using Domain.Notificaciones;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class NotificacionConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable("notificaciones");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(r => r.NotificadoId).HasColumnName("usuario_notificado_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(r => r.NotificadoId);

            builder
                .HasDiscriminator<string>("tipo_de_interaccion")
                .HasValue<HiloSeguidoNotificacion>("hilo_seguido")
                .HasValue<HiloComentadoNotificacion>("hilo_comentado")
                .HasValue<ComentarioRespondidoNotificacion>("comentario_respondido");
        }
    }

    public class HiloInteraccionNotificacionConfiguration : IEntityTypeConfiguration<HiloInteraccionNotificacion>
    {
        public void Configure(EntityTypeBuilder<HiloInteraccionNotificacion> builder)
        {
            builder.Property(r => r.HiloId).HasColumnName("hilo_id");

            builder.HasOne<Hilo>().WithMany(h=> h.Notificaciones).HasForeignKey(r => r.HiloId);

            builder.Property(r => r.ComentarioId).HasColumnName("comentario_id");
            builder.HasOne<Comentario>().WithMany().HasForeignKey(r => r.ComentarioId);

            
        }
    }

    public class HiloComentadoNotificacionConfiguration : IEntityTypeConfiguration<HiloComentadoNotificacion>
    {
        public void Configure(EntityTypeBuilder<HiloComentadoNotificacion> builder)
        {
        }
    }

    public class HiloSeguidoNotificacionConfiguration : IEntityTypeConfiguration<HiloSeguidoNotificacion>
    {
        public void Configure(EntityTypeBuilder<HiloSeguidoNotificacion> builder)
        {
        }
    }

    public class ComentarioRespondidoNotificacionConfiguration : IEntityTypeConfiguration<ComentarioRespondidoNotificacion>
    {
        public void Configure(EntityTypeBuilder<ComentarioRespondidoNotificacion> builder)
        {
            builder.Property(r => r.RespondidoId).HasColumnName("respondido_id");
            builder.HasOne<Comentario>().WithMany().HasForeignKey(r => r.RespondidoId);
        }

    }
}