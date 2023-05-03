﻿namespace TestPlatform.Database.Seed.Seeders.Authorization
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Seed.BindingModels.Authorization;
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
                await userRoleMapService.CreateAsync<UserRoleMap, SeedUserRoleMapBM>(dto);
            }
        }
    }
}