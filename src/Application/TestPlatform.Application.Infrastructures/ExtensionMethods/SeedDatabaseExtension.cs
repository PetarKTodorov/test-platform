namespace TestPlatform.Application.Infrastructures.ExtensionMethods
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using TestPlatform.Database.Seed;

    public static class SeedDatabaseExtension
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            if (ApplicationDbSeeder.IsNotSeeded(serviceScope.ServiceProvider))
            {
                await ApplicationDbSeeder.SeedAsync(serviceScope.ServiceProvider);
            }
        }
    }
}
