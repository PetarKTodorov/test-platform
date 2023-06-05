namespace TestPlatform.Database.Seed.Seeders.Rooms
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Seed.BindingModels.Rooms;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Database.Rooms.Interfaces;

    internal class RoomSeeder : BaseSeeder
    {
        public RoomSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IRoomService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedRoomBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<BaseBM, SeedRoomBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
