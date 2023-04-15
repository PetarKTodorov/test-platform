namespace TestPlatform.Database.Seed.Seeders.Authorization
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Seed.Interfaces;
    using TestPlatform.DTOs.BindingModels.Authorization;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    public class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleService = serviceProvider.GetRequiredService<IRoleService>();

            string jsonFileName = "roles";

            var dtoObjects = await Deserializer.DeserializeAsync<RoleBM>(jsonFileName);

            foreach (var dto in dtoObjects)
            {
                await roleService.CreateAsync<Role, RoleBM>(dto);
            }
        }
    }
}
