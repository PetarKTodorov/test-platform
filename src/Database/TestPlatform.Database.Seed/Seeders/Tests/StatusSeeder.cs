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

    internal class StatusSeeder : BaseSeeder
    {
        public StatusSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var statusService = this.ServiceProvider.GetRequiredService<IStatusService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedStatusBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                await statusService.CreateAsync<BaseBM, SeedStatusBM>(dto, administratorId);
            }
        }
    }
}
