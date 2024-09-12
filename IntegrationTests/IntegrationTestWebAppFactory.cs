using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Persistence;

namespace IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _db = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("blog_testing")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build()
        ;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<BlogDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<BlogDbContext>(options =>
                {
                    options.UseNpgsql(_db.GetConnectionString());
                });
            });
        }
        public Task InitializeAsync()
        {
            return _db.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _db.StopAsync();
        }
    }
}