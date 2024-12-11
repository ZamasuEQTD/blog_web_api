using Application.Medias.Abstractions;
using Application.Medias.Services;
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

            services.AddScoped<IMediaFactory, MediaFactoryServiceScoped>();

            services.AddScoped<VideoService>();
            services.AddScoped<ImagenService>();
            services.AddScoped<MediaProcesador>();
            services.AddScoped<GifService>();


            return services;
        }
    }
}