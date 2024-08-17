using Domain.Hilos;
using Domain.Stickies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class StickyConfiguration : IEntityTypeConfiguration<Sticky>
    {
        public void Configure(EntityTypeBuilder<Sticky> builder)
        {
            builder.ToTable("stickies");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

            builder.Property(s => s.Hilo).HasColumnName("hilo_id");
            builder.HasOne<Hilo>().WithOne().HasForeignKey<Sticky>(s => s.Hilo);

            builder.Property(s => s.Conluye).HasColumnName("concluye_at");

            builder.Property(s => s.Status).HasColumnName("status");
        }
    }
}