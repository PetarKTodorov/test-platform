﻿namespace TestPlatform.Database.Seed.Seeders.Questions
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Seed.BindingModels.Questions;
    using TestPlatform.Services.Database.Questions.Interfaces;

    internal class QuestionsAnswersMapSeeeder : BaseSeeder
    {
        public QuestionsAnswersMapSeeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IQuestionAnswerMapService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedQuestionAnswersMapBM>(this.JsonFileName, this.Logger);

            var administratorId = new Guid(GlobalConstants.ADMINISTRATOR_ID);
            foreach (var dto in dtoObjects)
            {
                await service.CreateAsync<QuestionAnswerMap, SeedQuestionAnswersMapBM>(dto, administratorId);
            }
        }
    }
}