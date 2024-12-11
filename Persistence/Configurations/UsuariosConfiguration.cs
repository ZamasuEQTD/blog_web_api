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
            builder.Property(u => u.HashedPassword).HasColumnName("hashed_password")    ;
        }
    }
}