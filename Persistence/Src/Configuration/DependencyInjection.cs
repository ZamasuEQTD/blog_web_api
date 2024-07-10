using Application.Abstractions.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Usuarios;
using Persistence.Repositories;
using Domain.Media;
using Domain.Hilos.Abstractions;
using Domain.Media.Abstractions;
using Domain.Encuestas.Abstractions;
using Domain.Usuarios.Abstractions;

namespace Persistence.Configuration
{
     static public class DependencyInjection
    {
        static public IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
           {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
           });

            services.AddRepositories();

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }


        static public IServiceCollection AddRepositories(this IServiceCollection services )
        {
            services.AddScoped<IHilosRepository,HilosRepository>();
            services.AddScoped<IMediasRepository,MediasRepository>();
            services.AddScoped<IEncuestasRepository,EncuestasRepository>();
            services.AddScoped<IUsuariosRepository,UsuariosRepository>();

            return services;
        }
}
}