namespace TestPlatform.Database.Seed.Interfaces
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync();
    }
}
