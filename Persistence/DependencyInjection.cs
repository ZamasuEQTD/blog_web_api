using Application.Abstractions.Data;
using Domain.Baneos.Abstractions;
using Domain.Categorias.Abstractions;
using Domain.Comentarios;
using Domain.Encuestas.Abstractions;
using Domain.Hilos.Abstractions;
using Domain.Usuarios.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Repositories;

namespace Persistence.Configuration
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDBConnectionFactory, NpgsqlConnectionFactory>();

            services.AddScoped<IHilosRepository, HilosRepository>();
            services.AddScoped<IBaneosRepository, BaneosRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<ICategoriasRepository, CategoriasRepository>();
            services.AddScoped<IComentariosRepository, ComentariosRepository>();
            services.AddScoped<IEncuestasRepository, EncuestasRepository>();

            return services;
        }
    }
}