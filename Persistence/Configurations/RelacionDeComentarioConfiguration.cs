using Domain.Comentarios;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class RelacionDeComentarioConfiguration : IEntityTypeConfiguration<RelacionDeComentario>
    {
        public void Configure(EntityTypeBuilder<RelacionDeComentario> builder)
        {
            builder.ToTable("relaciones_de_comentarios");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(r => r.UsuarioId).HasColumnName("usuario_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(r => r.UsuarioId);

            builder.Property(r => r.ComentarioId).HasColumnName("comentario_id");
            builder.HasOne<Comentario>().WithMany().HasForeignKey(r => r.ComentarioId);

            builder.Property(r => r.Oculto).HasColumnName("oculto");

        }
    }
}