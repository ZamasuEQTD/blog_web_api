using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Features.Medias.Models;
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

            builder.Property(c => c.HiloId).HasColumnName("hilo_id");
            builder.HasOne<Hilo>().WithMany().HasForeignKey(c => c.HiloId);

            builder.Property(c => c.MediaSpoileableId).HasColumnName("media_spoileable_id");
            builder.HasOne<MediaSpoileable>().WithMany().HasForeignKey(c => c.MediaSpoileableId);

            builder.Property(c => c.Texto).HasConversion(text => text.Value, value => Texto.Create(value).Value);

            builder.OwnsOne(c => c.Status, b =>
            {
                b.Property(c => c.Value).HasColumnName("status");
            });

            builder.OwnsOne(c => c.Color, b =>
            {
                b.Property(c => c.Value).HasColumnName("color");
            });

            builder.Property(c => c.Texto).HasColumnName("texto");

            builder.OwnsOne(c => c.Autor, b =>
            {
                b.Property(c => c.Nombre).HasColumnName("autor_nombre");
                b.Property(c => c.Rango).HasColumnName("autor_rango");
            });

            builder.Property(c => c.Tag).HasConversion(tag => tag.Value, value => Tag.Create(value).Value).HasColumnName("tag");
            builder.Property(c => c.TagUnico).HasConversion(tagUnico => tagUnico.Value, value => TagUnico.Create(value).Value).HasColumnName("tag_unico");
            builder.Property(c => c.Dados).HasConversion(dados => dados.Value, value => Dados.Create(value).Value).HasColumnName("dados");

            builder.Property(c => c.RecibirNotificaciones).HasColumnName("recibir_notificaciones");

            builder.Property(c => c.CreatedAt).HasColumnName("created_at")  ;

            builder.OwnsMany(c => c.Respuestas, y =>
            {
                y.ToTable("respuestas_comentarios");

                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(c => c.RespondidoId).HasColumnName("respondido_id");
                y.WithOwner().HasForeignKey(c => c.RespondidoId);

                y.Property(c => c.RespuestaId).HasColumnName("respuesta_id")    ;
                y.HasOne<Comentario>().WithMany().HasForeignKey(c => c.RespuestaId);
            });

            builder.OwnsMany(c => c.Denuncias, y =>
            {
                y.ToTable("denuncias_de_comentario");

                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(h => h.ComentarioId).HasColumnName("comentario_id");
                y.WithOwner().HasForeignKey(d => d.ComentarioId);

                y.Property(h => h.DenuncianteId).HasColumnName("denunciante_id");
                y.HasOne<Usuario>().WithMany().HasForeignKey(h => h.DenuncianteId);

                y.Property(h => h.Status);
            });

            builder.OwnsMany(c => c.Relaciones, y =>
            {
                y.ToTable("comentario_interacciones");

                y.HasKey(c => c.Id);
                y.Property(c => c.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id")    ;

                y.Property(r => r.UsuarioId).HasColumnName("usuario_id");
                y.HasOne<Usuario>().WithMany().HasForeignKey(r => r.UsuarioId);

                y.Property(r => r.ComentarioId).HasColumnName("comentario_id");
                y.WithOwner().HasForeignKey(c => c.ComentarioId);

                y.Property(r => r.Oculto).HasColumnName("oculto");
            });
        }
    }
}