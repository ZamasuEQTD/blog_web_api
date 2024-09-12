using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly BlogDbContext Context;
        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            Sender = _scope.ServiceProvider.GetService<ISender>()!;

            Context = _scope.ServiceProvider.GetService<BlogDbContext>()!;
        }
    }
}