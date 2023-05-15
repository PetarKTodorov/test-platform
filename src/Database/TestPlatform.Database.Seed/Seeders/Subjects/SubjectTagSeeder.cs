namespace TestPlatform.Database.Seed.Seeders.Subjects
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Seed.BindingModels.Subjects;
    using TestPlatform.Services.Database.Subjects.Interfaces;

    internal class SubjectTagSeeder : BaseSeeder
    {
        public SubjectTagSeeder(IServiceProvider serviceProvider, ILogger logger, string jsonFileName)
            : base(serviceProvider, logger, jsonFileName)
        {
        }

        public override async Task SeedAsync()
        {
            var subjectTagService = this.ServiceProvider.GetRequiredService<ISubjectTagService>();

            var dtoObjects = await Deserializer.DeserializeAsync<SeedCreateSubjectTagBM>(this.JsonFileName, this.Logger);

            foreach (var dto in dtoObjects)
            {
                await subjectTagService.CreateAsync<SubjectTag, SeedCreateSubjectTagBM>(dto);
            }
        }
    }
}
