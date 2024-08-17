using Domain.Categorias;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class HiloConfiguration : IEntityTypeConfiguration<Hilo>
    {
        public void Configure(EntityTypeBuilder<Hilo> builder)
        {
            builder.ToTable("hilos");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(h => h.AutorId).HasColumnName("autor_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(h => h.AutorId);

            builder.Property(h => h.Categoria).HasColumnName("subcategoria_id");
            builder.HasOne<Subcategoria>().WithMany().HasForeignKey(h => h.Categoria);

            builder.Property(h => h.Encuesta).HasColumnName("encuesta_id");
            builder.HasOne<Encuesta>().WithOne().HasForeignKey<Hilo>(h => h.Encuesta);

            builder.ComplexProperty(h => h.Titulo).Property(t => t.Value).HasColumnName("titulo");
            builder.ComplexProperty(h => h.Descripcion).Property(t => t.Value).HasColumnName("descripcion");

            builder.Property(h => h.Status).HasColumnName("status");

            builder.Property(h => h.RecibirNotificaciones).HasColumnName("recibir_notificaciones");

            builder.Property(h => h.UltimoBump).HasColumnName("ultimo_bump");

            builder.OwnsOne(h => h.Configuracion, conf =>
            {
                conf.Property(c => c.Dados).HasColumnName("dados");
                conf.Property(c => c.IdUnicoActivado).HasColumnName("id_unico_activado");
            });
        }
    }
}