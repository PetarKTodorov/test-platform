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

    internal class GradeScaleSeeder : BaseSeeder
    {
        public GradeScaleSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IGradeScaleService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedGradeScaleBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<BaseBM, SeedGradeScaleBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
