namespace TestPlatform.Database.Seed.Seeders.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Seed.BindingModels.Tests;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Database.Test.Interfaces;

    internal class TestSeeder : BaseSeeder
    {
        public TestSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var testService = this.ServiceProvider.GetRequiredService<ITestService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedTestBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await testService.CreateAsync<BaseBM, SeedTestBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
