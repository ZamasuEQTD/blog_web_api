using Application.Abstractions;
using Application.Abstractions.Data;
using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Usuarios.Abstractions;
using Infraestructure.Authentication;
using Infraestructure.Data;
using Infraestructure.Media;
using Infraestructure.Providers;
using Infraestructure.Usuarios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharedKernel.Abstractions;

namespace Infraestructure.Configuration
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IUserContext,UserContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddSingleton<IPasswordHasher,BCryptPasswordHasher>();
            services.AddSingleton<IVistaPreviaService, FfmpegVideoVistaPreviaService>();
            services.AddSingleton<IFileService,FilesService>();
            services.AddSingleton<IResizer, Resizer>();
            services.AddSingleton<IFolderProvider, FolderProvider>();    
            services.AddSingleton<IMediaHasher,MediaHasher>();        
            return services;
        }
        static internal IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}