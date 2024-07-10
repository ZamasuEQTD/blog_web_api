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
            builder.HasOne<Media>().WithMany().HasForeignKey(m=> m.MediaId);
        }
    }

    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
      
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("medias");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
            builder.Property(m => m.Hash).HasColumnName("hash").IsRequired();
        }
    }
}