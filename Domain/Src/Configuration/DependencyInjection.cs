using Domain.Comentarios.Abstractions;
using Domain.Comentarios.Services;
using Domain.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddDomain(this IServiceCollection services){
            services.AddSingleton<IRandomGeneratorService,RandomGeneratorService>();
            services.AddSingleton<IRandomTextService,RandomTextService>();

            services.AddSingleton<ITagGenerator,TagGenerator>();
            services.AddSingleton<IDadosGenerator>();
            return services;
        }
    }   
}