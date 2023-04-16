﻿namespace TestPlatform.Database.Seed.Seeders.Authorization
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.BindingModels.Authorization;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    internal class RolesSeeder : BaseSeeder
    {
        public RolesSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var roleService = this.ServiceProvider.GetRequiredService<IRoleService>();

            var dtoObjects = await Deserializer.DeserializeAsync<RoleBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                await roleService.CreateAsync<Role, RoleBM>(dto);
            }
        }
    }
}
