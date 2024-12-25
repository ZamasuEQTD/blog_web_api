using Domain.Baneos;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Encuestas;
using Domain.Features.Medias.Models;
using Domain.Hilos;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Persistence
{
    public class BlogDbContext : IdentityDbContext<Usuario, Role, UsuarioId>
    {
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _hasher;
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Hilo> Hilos { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Baneo> Baneos { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<HiloInteraccion> Relaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<MediaSpoileable> MediaSpoileables { get; set; }
        public DbSet<HashedMedia> Medias { get; set; }
        public BlogDbContext(DbContextOptions options, IMediator mediator, IPasswordHasher hasher) : base(options)
        {
            _mediator = mediator;
            _hasher = hasher;
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
        
            base.OnModelCreating(modelBuilder);

            Usuario moderador = new Usuario
            {
                Id = new UsuarioId(Guid.NewGuid()),
                UserName = "Moderador",
                PasswordHash = _hasher.Hash(Password.Create("Moderador").Value),
                Moderador = "Zamasus",
                RegistradoEn = DateTime.UtcNow
            };

            Usuario owner = new Usuario
            {
                Id = new UsuarioId(Guid.NewGuid()),
                UserName = "Owner1223",
                PasswordHash = _hasher.Hash(Password.Create("Owner123").Value),
                Moderador = "Zamasu",
                RegistradoEn = DateTime.UtcNow
            };

            modelBuilder.Entity<Usuario>(entity =>
            {

                entity.HasData(
                    moderador,
                    owner
                );
                entity.ToTable(name: "usuarios");         

                entity.Property(b => b.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");

                entity.Property(u=> u.UserName).HasColumnName("username");

                entity.Property(u=> u.RegistradoEn).HasColumnName("registrado_en");

                entity.Property(e => e.Moderador).HasColumnName(name:"moderador");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(Role.Roles);

                entity.ToTable(name: "roles");

                entity.Property(b => b.Id).HasConversion(id => id.Value, value => new(value)).HasColumnName("id");
                
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {

                entity.HasData(new IdentityUserRole<string>
                {
                    UserId = moderador.Id.ToString(),
                    RoleId = Role.Moderador.Id.ToString()
                }
                , new IdentityUserRole<string>
                {
                    UserId = owner.Id.ToString(),
                    RoleId = Role.Owner.Id.ToString()
                }
                );
            
                entity.ToTable("usuario_roles");
                
                entity.HasKey(p => new {p.UserId, p.RoleId});

                entity.Property(b => b.UserId).HasColumnName("usuario_id");
                entity.Property(b => b.RoleId).HasColumnName("role_id");
            });
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