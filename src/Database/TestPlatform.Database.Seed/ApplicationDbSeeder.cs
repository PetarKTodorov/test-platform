namespace TestPlatform.Database.Seed
{
    using Microsoft.Extensions.DependencyInjection;
    using TestPlatform.Database.Seed.Interfaces;
    using TestPlatform.Database.Seed.Seeders.Authorization;

    public static class ApplicationDbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                {
                    new RolesSeeder(),
                };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(serviceProvider);
            }
        }

        public static bool IsNotSeeded(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<TestPlatformDbContext>();

            bool isNotSeeded = dbContext.Roles.Count() == 0;

            return isNotSeeded;
        }
    }
}
