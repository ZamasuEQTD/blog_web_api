using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApi.Extensions
{
    static public class MigrationsExtensions
    {
        static public void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

            db.Database.Migrate();
        }
    }

}