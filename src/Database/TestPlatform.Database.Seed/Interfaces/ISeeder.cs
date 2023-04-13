namespace TestPlatform.Database.Seed.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
