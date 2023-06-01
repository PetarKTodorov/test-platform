namespace TestPlatform.Application.Infrastructures.ExtensionMethods
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using TestPlatform.Database;
    using TestPlatform.Database.Seed;

    public static class SeedDatabaseExtension
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var databaseContext =
                serviceScope.ServiceProvider.GetRequiredService<TestPlatformDbContext>();

            using var dbContextTransaction = databaseContext.Database.BeginTransaction();
            try
            {
                if (ApplicationDbSeeder.IsNotSeeded(serviceScope.ServiceProvider))
                {
                    await ApplicationDbSeeder.SeedAsync(serviceScope.ServiceProvider);
                }

                await dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();
            }
        }
    }
}
