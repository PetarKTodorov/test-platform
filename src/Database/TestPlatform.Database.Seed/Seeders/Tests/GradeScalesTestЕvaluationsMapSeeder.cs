﻿namespace TestPlatform.Database.Seed.Seeders.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Seed.BindingModels.Tests;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Database.Test.Interfaces;

    internal class GradeScalesTestEvaluationsMapSeeder : BaseSeeder
    {
        public GradeScalesTestEvaluationsMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IGradeScaleTestEvaluationMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedGradeScaleTestEvaluationMapBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<BaseBM, SeedGradeScaleTestEvaluationMapBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
