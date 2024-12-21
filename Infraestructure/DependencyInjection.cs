using Application.Abstractions;
using Application.Medias.Abstractions;
using Application.Medias.Services;
using Domain.Usuarios.Abstractions;
using Infraestructure.Authentication;
using Infraestructure.Hubs.Abstractions;
using Infraestructure.Media;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;
using static Application.Medias.Services.VideoService;

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

            services.AddScoped<IMediaFolderProvider, FolderProvider>();
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<IVideoGifPrevisualizadorService, FfmpegVideoVistaPreviaService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IImageResizer, Resizer>();

            services.AddScoped<MiniaturaService>();

            services.AddScoped<GifVideoPrevisualizadorProcesador>();
            return services;
        }
    }
}