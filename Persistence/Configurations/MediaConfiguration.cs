using Domain.Media;
using Domain.Media;
using Domain.Media.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{

    public class MediaReferenceConfiguration : IEntityTypeConfiguration<MediaReference>
    {
        public void Configure(EntityTypeBuilder<MediaReference> builder)
        {
            builder.ToTable("media_references");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(e => e.Spoiler).HasColumnName("spoiler");

            builder.Property(r => r.MediaId).HasColumnName("media_id");
            builder.HasOne<HashedMedia>().WithMany().HasForeignKey(r => r.MediaId);
        }
    }
    public class MediaConfiguration : IEntityTypeConfiguration<HashedMedia>
    {
        public void Configure(EntityTypeBuilder<HashedMedia> builder)
        {
            builder.ToTable("media");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.HasIndex(e => e.Hash).IsUnique();
            builder.Property(e => e.Hash).HasColumnName("hash");

            builder.Property(e => e.Path).HasColumnName("path");

            builder.HasDiscriminator<string>("tipo_de_archivo")
            .HasValue<Video>("video")
            .HasValue<YoutubeVideo>("youtube")
            .HasValue<Imagen>("imagen")
            ;
        }
    }

    public class FileMediaConfiguration : IEntityTypeConfiguration<FileMedia>
    {
        public void Configure(EntityTypeBuilder<FileMedia> builder)
        {
            builder.Property(e => e.FileName).HasColumnName("filename");
            builder.OwnsOne(e => e.Source, y =>
            {
                y.Property(y => y.Value).HasColumnName("media_source");
            });
        }
    }
    public class NetworkMediaConfiguration : IEntityTypeConfiguration<NetworkMedia>
    {
        public void Configure(EntityTypeBuilder<NetworkMedia> builder)
        {
            builder.OwnsOne(e => e.Source, y =>
            {
                y.Property(y => y.Value).HasColumnName("network_source");
            });
        }
    }
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.Property(e => e.Miniatura).HasColumnName("miniatura");
            builder.Property(e => e.Previsulizacion).HasColumnName("previsualizacion");
        }
    }
    public class ImagenConfiguration : IEntityTypeConfiguration<Imagen>
    {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            builder.Property(e => e.Miniatura).HasColumnName("miniatura");
        }
    }

    public class YoutubeVideoConfiguration : IEntityTypeConfiguration<YoutubeVideo>
    {
        public void Configure(EntityTypeBuilder<YoutubeVideo> builder)
        {
            builder.Property(e => e.Previsulizacion).HasColumnName("previsualizacion");
            builder.Property(e => e.Miniatura).HasColumnName("miniatura");
        }
    }
}