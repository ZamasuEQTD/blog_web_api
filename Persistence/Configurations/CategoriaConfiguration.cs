namespace Persistence.Configurations
{
    using Domain.Categorias;
    using Domain.Hilos;
    using Domain.Stickies;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Persistence.Configurations
    {
        internal sealed class CategoriasConfiguration : IEntityTypeConfiguration<Categoria>
        {
            public void Configure(EntityTypeBuilder<Categoria> builder)
            {
                builder.ToTable("categorias");

                builder.HasKey(s => s.Id);
                builder.Property(s => s.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                builder.Property(s => s.Nombre).HasColumnName("nombre") ;
                builder.Property(s => s.OcultoPorDefecto).HasColumnName("oculto_por_defecto");

                builder.HasMany(s => s.Subcategorias).WithOne();   
            }
        }
    }

    public class SubcategoriaConfiguration : IEntityTypeConfiguration<Subcategoria>
    {
        public void Configure(EntityTypeBuilder<Subcategoria> builder)
        {
            builder.ToTable("subcategorias");

            builder.HasOne<Categoria>().WithMany(s => s.Subcategorias).HasForeignKey("categoria_id");

            builder.Property(s => s.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            builder.Property(s => s.NombreCorto).HasColumnName("nombre_corto");
            builder.Property(s => s.Nombre).HasColumnName("nombre");
        }
    }
}