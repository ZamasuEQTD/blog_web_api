namespace Persistence.Configurations
{
    using Domain.Categorias;
    using Domain.Hilos;
    using Domain.Stickies;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Persistence.Configurations
    {
        internal sealed class SubcategoriasConfiguration : IEntityTypeConfiguration<Subcategoria>
        {
            public void Configure(EntityTypeBuilder<Subcategoria> builder)
            {
                builder.ToTable("subcategorias");

                builder.HasKey(s => s.Id);
                builder.Property(s => s.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                builder.HasOne<Categoria>().WithMany().HasForeignKey(s => s.Categoria);
                builder.Property(s => s.Categoria).HasColumnName("categoria_id");

                builder.Property(s => s.Nombre).HasColumnName("nombre");
                builder.Property(s => s.NombreCorto).HasColumnName("nombre_corto");

            }
        }
    }
}