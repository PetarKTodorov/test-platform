namespace TestPlatform.Database.Seed.Seeders.Subjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Seed.BindingModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    internal class TestSubjectTagMapSeeder : BaseSeeder
    {
        public TestSubjectTagMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var testSubjectTagMapService = this.ServiceProvider.GetRequiredService<ITestSubjectTagMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedTestSubjectTagMapBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                await testSubjectTagMapService.CreateAsync<TestSubjectTagMap, SeedTestSubjectTagMapBM>(dto, administratorId);
            }
        }
    }
}
