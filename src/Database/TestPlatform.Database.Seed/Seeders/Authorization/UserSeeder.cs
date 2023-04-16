namespace TestPlatform.Database.Seed.Seeders.Authorization
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.BindingModels.Authorization;
    using TestPlatform.Services.Database.Authorization.Interfaces;

    internal class UserSeeder : BaseSeeder
    {
        public UserSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var userService = this.ServiceProvider.GetRequiredService<IUserService>();

            var dtoObjects = await Deserializer.DeserializeAsync<CreateUserBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                await userService.CreateAsync<User, CreateUserBM>(dto);
            }
        }
    }
}
