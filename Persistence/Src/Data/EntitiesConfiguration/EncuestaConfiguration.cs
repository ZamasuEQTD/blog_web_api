using Domain.Encuestas;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class EncuestaConfiguration : IEntityTypeConfiguration<Encuesta>
    {
        public void Configure(EntityTypeBuilder<Encuesta> builder)
        {

            builder.ToTable("encuestas");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.HasMany(e=> e.Respuestas).WithOne().HasForeignKey(r=> r.EncuestaId);
            builder.HasMany(e=> e.Votos).WithOne().HasForeignKey(v=> v.EncuestaId);

            builder.Property(h => h.CreatedAt).HasColumnName("created_at");
        }
    }

    public class RespuestaConfiguration : IEntityTypeConfiguration<Respuesta>
    {
        public void Configure(EntityTypeBuilder<Respuesta> builder)
        {

            builder.ToTable("respuestas");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            
            builder.HasOne<Encuesta>().WithMany(e => e.Respuestas).HasForeignKey(r=> r.EncuestaId);
        }
    }

    public class VotoConfiguration : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {

            builder.ToTable("votos");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            
            builder.HasOne<Encuesta>().WithMany(e => e.Votos).HasForeignKey(r=> r.EncuestaId);
            builder.HasOne<Usuario>().WithMany().HasForeignKey(v=> v.VotanteId);
            builder.HasOne<Respuesta>().WithMany().HasForeignKey(r=> r.RespuestaId);
        }
    }
}