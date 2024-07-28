using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application.Encuestas.Services;
using Application.Hilos.Commands;

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
        
            services.AddScoped<MediaProcesor>();
            services.AddSingleton<EncuestaOrchetastor>();

            return services;
        }
    }
}