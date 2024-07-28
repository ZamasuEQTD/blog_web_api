using Domain.Comentarios.Abstractions;
using Domain.Comentarios.Services;
using Domain.Common.Abstractions;
using Domain.Media.Abstractions.Strategies;
using Domain.Media.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    static public class DependencyInjection
    {
        static public IServiceCollection AddDomain(this IServiceCollection services){
            services.AddScoped<IMediaProcesorsStrategy,VideoProcesorStrategy>();
            services.AddScoped<PrevisualizadorProcesor>();
            services.AddScoped<MiniaturaProcesor>();
            services.AddSingleton<LanzadorDeDados>();
            services.AddSingleton<TagGenerator>();
            services.AddSingleton<IRandomGeneratorService,RandomGeneratorService>();
            services.AddSingleton<IRandomTextGenerator,RandomTextGenerator>();
            return services;
        }
    }   
}