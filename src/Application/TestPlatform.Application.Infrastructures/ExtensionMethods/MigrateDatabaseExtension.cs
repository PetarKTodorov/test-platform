namespace TestPlatform.Application.Infrastructures.ExtensionMethods
{
    using System.Threading.Tasks;

    using TestPlatform.Database;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class MigrateDatabaseExtension
    {
        public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var databaseContext =
                serviceScope.ServiceProvider.GetRequiredService<TestPlatformDbContext>();

            await databaseContext.Database.MigrateAsync();
        }
    }
}
