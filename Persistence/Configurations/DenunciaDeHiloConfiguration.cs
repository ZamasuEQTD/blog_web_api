using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class DenunciaDeHiloHiloConfiguration : IEntityTypeConfiguration<DenunciaDeHilo>
    {
        public void Configure(EntityTypeBuilder<DenunciaDeHilo> builder)
        {
            builder.ToTable("denuncias_de_hilo");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(h => h.HiloId).HasColumnName("hilo_id");
            builder.HasOne<Hilo>().WithMany().HasForeignKey(h => h.HiloId);

            builder.Property(h => h.DenuncianteId).HasColumnName("denunciante_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);

            builder.Property(h => h.Status).HasColumnName("status");
        }
    }
}