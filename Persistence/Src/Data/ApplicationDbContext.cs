using Application.Abstractions.Data;
using Domain.Hilos;
using Domain.Media;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher _publisher;
    
    public DbSet<Hilo> Hilos { get; set; }
    public DbSet<MediaReference> MediaReferences { get; set; }
    public DbSet<Media> Medias { get; set; }

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        var domainEvents = ChangeTracker.Entries<Entity>().Select(e => e.Entity).Where(e => e.DomainEvents.Any()).SelectMany(e => e.DomainEvents);
        foreach (var evento in domainEvents.ToList())
        {
            await _publisher.Publish(evento, cancellationToken);
        }

        return result;
    }   
}