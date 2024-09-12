using Application.Abstractions;
using Application.Medias.Abstractions;
using Application.Medias.Services;
using Domain.Media.Abstractions;
using Domain.Usuarios.Abstractions;
using Infraestructure.Authentication;
using Infraestructure.Media;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;

namespace Infraestructure.Configuration
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            services.AddScoped<IFolderProvider, FolderProvider>();
            services.AddScoped<IHasherCalculator, HasherCalculator>();
            services.AddScoped<IVideoPrevisualizadorGenerador, FfmpegVideoVistaPreviaService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IResizer, Resizer>();

            services.AddScoped<MiniaturaProcesor>();
            services.AddScoped<GifVideoPrevisualizadorProcesador>();

            return services;
        }
    }
}