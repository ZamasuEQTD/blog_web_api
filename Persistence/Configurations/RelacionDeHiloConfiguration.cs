using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RelacionDeHiloConfiguration : IEntityTypeConfiguration<RelacionDeHilo>
    {
        public void Configure(EntityTypeBuilder<RelacionDeHilo> builder)
        {
            builder.ToTable("relaciones_de_hilo");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(d => d.HiloId).HasColumnName("hilo_id");
            builder.HasOne<Hilo>().WithMany().HasForeignKey(d => d.HiloId);

            builder.Property(r => r.UsuarioId).HasColumnName("usuario_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(r => r.UsuarioId);

            builder.Property(r => r.Seguido).HasColumnName("seguido");
            builder.Property(r => r.Favorito).HasColumnName("favorito");
            builder.Property(r => r.Oculto).HasColumnName("oculto");
        }
    }
}