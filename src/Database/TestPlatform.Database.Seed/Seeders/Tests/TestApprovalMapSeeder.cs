namespace TestPlatform.Database.Seed.Seeders.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Database.Seed.BindingModels.Tests;
    using TestPlatform.Services.Database.Test.Interfaces;

    internal class TestApprovalMapSeeder : BaseSeeder
    {
        public TestApprovalMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var testApprovalMapService = this.ServiceProvider.GetRequiredService<ITestApprovalMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedTestApprovalMapBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                await testApprovalMapService.CreateAsync<TestApprovalMap, SeedTestApprovalMapBM>(dto, administratorId);
            }
        }
    }
}
