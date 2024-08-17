using Domain.Comentarios;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DenunciaDeComentarioConfiguration : IEntityTypeConfiguration<DenunciaDeComentario>
    {
        public void Configure(EntityTypeBuilder<DenunciaDeComentario> builder)
        {
            builder.ToTable("denuncias_de_comentario");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(h => h.ComentarioId).HasColumnName("comentarios_id");
            builder.HasOne<Comentario>().WithMany().HasForeignKey(h => h.ComentarioId);

            builder.Property(h => h.DenuncianteId).HasColumnName("denunciante_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);

            builder.Property(h => h.Status).HasColumnName("status");
        }
    }
}