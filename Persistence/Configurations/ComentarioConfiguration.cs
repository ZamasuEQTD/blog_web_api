using Domain.Comentarios;
using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("comentarios");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(c => c.AutorId).HasColumnName("autor_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(c => c.AutorId);

            builder.Property(c => c.Hilo).HasColumnName("hilo_id");
            builder.HasOne<Hilo>().WithMany().HasForeignKey(c => c.Hilo);

            builder.ComplexProperty(c => c.Texto).Property(t => t.Value).HasColumnName("encuesta_id");

            builder.Property(c => c.Status).HasColumnName("status");

            builder.Property(c => c.Destacado).HasColumnName("destacado");

            builder.Property(c => c.RecibirNotificaciones).HasColumnName("recibir_notificaciones");
        }
    }
}