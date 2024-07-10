using Microsoft.Extensions.DependencyInjection;
using MediatR;

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
            return services;
        }
    }
}