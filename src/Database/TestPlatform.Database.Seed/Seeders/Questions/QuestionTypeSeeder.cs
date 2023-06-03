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

    internal class QuestionTypeSeeder : BaseSeeder
    {
        public QuestionTypeSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var questionTypeService = this.ServiceProvider.GetRequiredService<IQuestionTypeService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedQuestionTypeBM>(this.JsonFileName, this.Logger);

            var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
            foreach (var dto in dtoObjects)
            {
                await questionTypeService.CreateAsync<BaseBM, SeedQuestionTypeBM>(dto, administratorId);
            }
        }
    }
}
