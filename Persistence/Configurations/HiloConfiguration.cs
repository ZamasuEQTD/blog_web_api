using Domain.Categorias;
using Domain.Comentarios;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Media;
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

            builder.Property(h => h.UltimoBump).HasColumnName("ultimo_bump");

            builder.OwnsOne(c => c.Status, b =>
            {
                b.Property(c => c.Value).HasColumnName("status");
            });

            builder.Property(h => h.RecibirNotificaciones).HasColumnName("recibir_notificaciones");
            builder.ComplexProperty(h => h.Titulo).Property(t => t.Value).HasColumnName("titulo");
            builder.ComplexProperty(h => h.Descripcion).Property(t => t.Value).HasColumnName("descripcion");

            builder.Property(r => r.AutorId).HasColumnName("usuario_id");
            builder.HasOne<Usuario>().WithMany().HasForeignKey(r => r.AutorId);

            builder.Property(h => h.Categoria).HasColumnName("subcategoria_id");
            builder.HasOne<Subcategoria>().WithMany().HasForeignKey(h => h.Categoria);

            builder.Property(r => r.PortadaId).HasColumnName("portada_id");
            builder.HasOne<MediaReference>().WithMany().HasForeignKey(r => r.PortadaId);

            builder.Property(h => h.Encuesta).HasColumnName("encuesta_id");
            builder.HasOne<Encuesta>().WithOne().HasForeignKey<Hilo>(h => h.Encuesta);

            builder.OwnsOne(h => h.Configuracion, conf =>
            {
                conf.Property(c => c.Dados).HasColumnName("dados");
                conf.Property(c => c.IdUnicoActivado).HasColumnName("id_unico_activado");
            });

            builder.OwnsOne(h => h.Sticky, y =>
            {
                y.ToTable("stickies");

                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(c => c.Conluye).HasColumnName("concluye");

                y.Property(c => c.Hilo).HasColumnName("hilo_id");
                y.WithOwner().HasForeignKey(s => s.Hilo);
            });

            builder.OwnsMany(h => h.ComentarioDestacados, y =>
            {
                y.ToTable("comentarios_destacados");

                y.HasKey(c => c.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.WithOwner().HasForeignKey(c => c.HiloId);
                y.Property(c => c.HiloId).HasColumnName("hilo_id");

                y.HasOne<Comentario>().WithMany().HasForeignKey(c => c.ComentarioId);
                y.Property(c => c.ComentarioId).HasColumnName("comentario_id");
            });


            builder.OwnsMany(h => h.Denuncias, y =>
            {
                y.ToTable("denuncias_de_hilo");

                y.HasKey(h => h.Id);
                y.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(d => d.HiloId).HasColumnName("hilo_id");
                y.WithOwner().HasForeignKey(d => d.HiloId);

                y.Property(d => d.DenuncianteId).HasColumnName("denunciante_id");
                y.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);

                y.Property(h => h.Status).HasColumnName("status");

                y.Property(h => h.Razon).HasColumnName("razon");
            });
        }
    }
}