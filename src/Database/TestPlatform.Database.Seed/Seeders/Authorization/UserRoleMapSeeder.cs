﻿namespace TestPlatform.Database.Seed.Seeders.Authorization
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Seed.BindingModels.Authorization;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    internal class UserRoleMapSeeder : BaseSeeder
    {
        public UserRoleMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var userRoleMapService = this.ServiceProvider.GetRequiredService<IUserRoleMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedUserRoleMapBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                await userRoleMapService.CreateAsync<BaseBM, SeedUserRoleMapBM>(dto, administratorId);
            }
        }
    }
}
