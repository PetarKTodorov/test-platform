namespace TestPlatform.Database.Seed.Seeders.Questions
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Seed.BindingModels.Questions;
    using TestPlatform.DTOs.BindingModels.Common;
    using TestPlatform.Services.Database.Questions.Interfaces;

    internal class QuestionTestMapSeeder : BaseSeeder
    {
        public QuestionTestMapSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IQuestionTestMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedQuestionTestMapBM>(this.JsonFileName, this.Logger);

            var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<BaseBM, SeedQuestionTestMapBM>(dto, administratorId);
            }
        }
    }
}
