using Domain.Notificaciones;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(u => u.CreatedAt).HasColumnName("created_at");

            builder.Property(u => u.Username).HasConversion(u => u.Value, value => Username.Create(value).Value).HasColumnName("username");
            builder.Property(u => u.HashedPassword).HasColumnName("password");

            builder.Property(u => u.Rango).HasConversion(r => r.Nombre, n => Rango.FromNombre(n)).HasColumnName("rango");
            
            builder.HasDiscriminator(u => u.Rango)
            .HasValue<Moderador>(Rango.Moderador)
            .HasValue<Anonimo>(Rango.Anonimo);

            //builder
            //.HasDiscriminator(u => u.Rango)
            //.HasValue<Moderador>(Usuario.RangoDeUsuario.Moderador)
            //.HasValue<Anonimo>(Usuario.RangoDeUsuario.Anonimo);
        }
    }

    public class AnonimoConfiguration : IEntityTypeConfiguration<Anonimo>
    {
        public void Configure(EntityTypeBuilder<Anonimo> builder)
        {

        }
    }

    public class ModeradorConfiguration : IEntityTypeConfiguration<Moderador>
    {
        public void Configure(EntityTypeBuilder<Moderador> builder)
        {

        }
    }
}