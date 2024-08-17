using Application.Abstractions;
using Domain.Usuarios.Abstractions;
using Infraestructure.Authentication;
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
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

            return services;
        }
    }
}