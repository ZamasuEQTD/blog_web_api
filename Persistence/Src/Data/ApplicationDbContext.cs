using Application.Abstractions.Data;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Media;
using Domain.Usuarios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher _publisher;
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Encuesta> Encuestas { get; set; }
    public DbSet<Voto> Votos { get; set; }
    public DbSet<Respuesta> Respuestas { get; set; }

    public DbSet<Hilo> Hilos { get; set; }
    public DbSet<MediaReference> MediaReferences { get; set; }
    public DbSet<MediaSource> MediaSources { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<MediaProvider> MediaProviers { get; set; }
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