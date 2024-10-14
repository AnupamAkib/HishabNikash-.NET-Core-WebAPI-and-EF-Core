using Microsoft.EntityFrameworkCore;
using Repository;

namespace HishabNikash.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                dbContext.Database.Migrate(); // Apply any pending migrations
            }

            return host;
        }
    }
}
