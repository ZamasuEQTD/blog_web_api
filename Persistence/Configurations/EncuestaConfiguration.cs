using Domain.Encuestas;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class EncuestaConfiguration : IEntityTypeConfiguration<Encuesta>
    {
        public void Configure(EntityTypeBuilder<Encuesta> builder)
        {
            builder.ToTable("encuestas");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.OwnsMany(e => e.Respuestas, y =>
            {
                y.ToTable("respuestas");
                y.HasKey(r => r.Id);
                y.Property(r => r.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(r => r.Contenido).HasColumnName("contenido");
            });

            builder.OwnsMany(e => e.Votos, y =>
            {
                y.ToTable("votos");
                y.HasKey(r => r.Id);
                y.Property(r => r.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                y.Property(r => r.VotanteId).HasColumnName("votante_id");
                y.HasOne<Usuario>().WithMany().HasForeignKey(v => v.VotanteId);

                y.Property(r => r.RespuestaId).HasConversion(id => id.Value, value => new(value)).HasColumnName("respuesta_id");

            });
        }
    }
}