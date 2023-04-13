namespace TestPlatform.Database.Seed
{
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

        public static bool IsNotSeeded()
        {
            return true;
        }
    }
}
