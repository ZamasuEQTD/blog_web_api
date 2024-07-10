using Domain.Encuestas;
using Domain.Hilos;
using Domain.Media;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class HiloConfiguration : IEntityTypeConfiguration<Hilo>
    {
        public void Configure(EntityTypeBuilder<Hilo> builder)
        {

            builder.ToTable("hilos");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.HasOne<Usuario>().WithOne().HasForeignKey<Hilo>(h=> h.AutorId);
            builder.HasOne<MediaReference>().WithOne().HasForeignKey<Hilo>(h=> h.PortadaId);
            builder.HasOne<Encuesta>().WithOne().HasForeignKey<Hilo>(h=> h.EncuestaId);
            builder.Property(h=>h.Titulo).HasColumnName("titulo");
            builder.Property(h=>h.Descripcion).HasColumnName("decripcion");

            builder.Property(h => h.CreatedAt).HasColumnName("created_at");

        }
    }
}