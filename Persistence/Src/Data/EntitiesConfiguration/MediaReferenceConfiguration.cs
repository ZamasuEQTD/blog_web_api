using Domain.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations{
    public class MediaReferenceConfiguration : IEntityTypeConfiguration<MediaReference>
    {
        public void Configure(EntityTypeBuilder<MediaReference> builder)
        {
            builder.ToTable("medias_references");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            builder.Property(m => m.Spoiler).HasColumnName("spoiler").IsRequired();
            builder.HasOne<MediaSource>().WithMany().HasForeignKey(m=> m.MediaId);
        }
    }

    public class MediaSourceConfiguration : IEntityTypeConfiguration<MediaSource> {
        public void Configure(EntityTypeBuilder<MediaSource> builder)
        {
            builder.ToTable("medias_sources");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            builder.HasOne<Media>().WithOne().HasForeignKey<MediaSource>(m=> m.Media);        
     
            builder.HasDiscriminator<string>("discriminator")
            .HasValue<HashedMedia>("hashed");
        }
    }

     

    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("medias");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");


            builder
            .HasDiscriminator<string>("discriminatro")
            .HasValue<Video>("video")
            .HasValue<Youtube>("youtube")
            .HasValue<Imagen>("imagen");
        }
    }

    public class VideoConfiguration : IEntityTypeConfiguration<Video> {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasOne<Imagen>().WithOne().HasForeignKey<Video>(v=> v.Previsualizacion);
            builder.HasOne<Imagen>().WithOne().HasForeignKey<Video>(v=> v.Thumbnail);
        }
    }

    public class YoutubeConfiguration : IEntityTypeConfiguration<Youtube>
    {
        public void Configure(EntityTypeBuilder<Youtube> builder)
        {
        }
    }

    public class ImagenConfiguration : IEntityTypeConfiguration<Imagen> {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            builder.HasOne<Imagen>().WithOne().HasForeignKey<Imagen>(v=> v.Thumbnail);
        }
    }
}