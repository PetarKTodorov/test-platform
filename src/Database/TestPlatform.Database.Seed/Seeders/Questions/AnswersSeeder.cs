namespace TestPlatform.Database.Seed.Seeders.Questions
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Seed.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;

    internal class AnswersSeeder : BaseSeeder
    {
        public AnswersSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IAnswerService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedAnswerBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<Answer, SeedAnswerBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
