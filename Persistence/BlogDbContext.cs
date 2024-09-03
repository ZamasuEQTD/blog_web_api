using Domain.Baneos;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Denuncias;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Stickies;
using Domain.Usuarios;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Persistence
{
    public class BlogDbContext : DbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Hilo> Hilos { get; set; }
        public DbSet<Baneo> Baneos { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<RelacionDeHilo> Relaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public BlogDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var r = await base.SaveChangesAsync(cancellationToken);

            await _mediator.DispatchDomainEventsAsync(this);

            return r;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }

    static public class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BlogDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}