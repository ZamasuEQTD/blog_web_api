using Domain.Baneos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BaneoConfiguration : IEntityTypeConfiguration<Baneo>
    {
        public void Configure(EntityTypeBuilder<Baneo> builder)
        {
            builder.ToTable("baneos");

            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(b => b.CreatedAt).HasColumnName("created_at");

            builder.Property(b => b.UsuarioBaneadoId).HasColumnName("usuario_baneado_id");
            
            builder.HasOne<Anonimo>().WithMany().HasForeignKey(b => b.UsuarioBaneadoId);

            builder.Property(b => b.ModeradorId).HasColumnName("moderador_id");

            builder.HasOne<Moderador>().WithMany().HasForeignKey(b => b.ModeradorId);

            builder.Property(b => b.Concluye).HasColumnName("concluye_en");

            builder.Property(b => b.Mensaje).HasColumnName("mensaje");
        }
    }
}