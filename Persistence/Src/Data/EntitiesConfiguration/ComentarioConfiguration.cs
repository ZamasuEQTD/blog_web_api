using Domain.Comentarios;
using Domain.Hilos;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("comentarios");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            builder.Property(m => m.Texto).HasColumnName("texto").IsRequired();
            builder.HasOne<Usuario>().WithMany().HasForeignKey(c=> c.AutorId);
            builder.HasOne<Hilo>().WithMany().HasForeignKey(c=> c.HiloId);
            builder.OwnsOne(c=> c.Informacion, info => {
                info.OwnsOne(d => d.Dados, dados=> {
                    dados.Property(d=> d.Value).HasColumnName("dados");
                });

                info.OwnsOne(d => d.Tag, tag=> {
                    tag.Property(d=> d.Value).HasColumnName("tag");
                });

                info.OwnsOne(d => d.TagUnico, tagUnico=> {
                    tagUnico.Property(d=> d.Value).HasColumnName("tag_unico");
                });
            });
        }
    }
}