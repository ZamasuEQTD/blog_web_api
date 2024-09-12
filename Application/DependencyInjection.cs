using Application.Medias.Abstractions;
using Application.Medias.Services;
using Domain.Media.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly))
            .Scan(x => x.FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces().WithScopedLifetime());

            services.AddKeyedScoped<IFileProcesorsStrategy, VideoProcesor>(FileType.Video);
            services.AddKeyedScoped<IFileProcesorsStrategy, GifProcesador>(FileType.Gif);
            services.AddKeyedScoped<IFileProcesorsStrategy, ImagenProcesor>(FileType.Imagen);

            services.AddScoped<IFileContextStrategy, FileContextStrategy>();

            services.AddScoped<MediaProcesador>();
            return services;
        }
    }
}