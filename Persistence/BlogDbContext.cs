using Domain.Baneos;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Denuncias;
using Domain.Encuestas;
using Domain.Hilos;
using Domain.Stickies;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Hilo> Hilos { get; set; }
        public DbSet<DenunciaDeHilo> DenunciasDeHilo { get; set; }
        public DbSet<DenunciaDeComentario> DenunciaDeComentarios { get; set; }
        public DbSet<RelacionDeComentario> RelacionesDeComentario { get; set; }
        public DbSet<RelacionDeHilo> RelacionesDeHilo { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Baneo> Baneos { get; set; }
        public DbSet<Sticky> Stickies { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }


        public BlogDbContext(DbContextOptions options) : base(options)
        {
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
}