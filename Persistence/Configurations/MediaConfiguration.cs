using Domain.Features.Medias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{

    public class SpoileableMediaConfiguration : IEntityTypeConfiguration<MediaSpoileable>
    {
        public void Configure(EntityTypeBuilder<MediaSpoileable> builder)
        {
            builder.ToTable("medias_spoileables");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(e => e.Spoiler).HasColumnName("spoiler");

            builder.Property(r => r.HashedMediaId).HasColumnName("hashed_media_id");
            builder.HasOne<HashedMedia>(m=> m.HashedMedia).WithMany().HasForeignKey(r => r.HashedMediaId);
        }
    }

    public class HashedMediaConfiguration : IEntityTypeConfiguration<HashedMedia>
    {
        public void Configure(EntityTypeBuilder<HashedMedia> builder)
        {
            builder.ToTable("medias");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
        
            builder.HasIndex(e => e.Hash).IsUnique();
            builder.Property(e => e.Hash).HasColumnName("hash");

            builder.Property(e => e.Filename).HasColumnName("filename");
            builder.Property(e => e.Url).HasColumnName("url");

            builder.OwnsOne(e => e.Media, m => {
                m.Property(e => e.Miniatura).HasColumnName("miniatura");
                m.Property(e => e.Previsualizacion).HasColumnName("previsualizacion")   ;
                m.OwnsOne(e => e.Provider, p => {
                    p.Property(e => e.Value).HasColumnName("provider");
                });
            });
        }
    }
}