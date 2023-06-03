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

    internal class QuestionsCopySeeder : BaseSeeder
    {
        public QuestionsCopySeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var service = this.ServiceProvider.GetRequiredService<IQuestionCopyService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedQuestionCopyBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                if (dto.CreatedBy == null)
                {
                    dto.CreatedBy = new Guid(GlobalConstants.ADMINISTRATOR_ID);
                }

                await service.CreateAsync<BaseBM, SeedQuestionCopyBM>(dto, dto.CreatedBy.Value);
            }
        }
    }
}
