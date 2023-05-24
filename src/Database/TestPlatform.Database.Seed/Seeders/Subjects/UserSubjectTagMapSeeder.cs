namespace TestPlatform.Database.Seed.Seeders.Subjects
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Seed.BindingModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    internal class UserSubjectTagMapSeeder : BaseSeeder
    {
        public UserSubjectTagMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var userSubjectTagMapService = this.ServiceProvider.GetRequiredService<IUserSubjectTagMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedUserSubjectTagMapBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                await userSubjectTagMapService.CreateAsync<UserSubjectTagMap, SeedUserSubjectTagMapBM>(dto);
            }
        }
    }
}
