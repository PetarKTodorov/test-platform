﻿namespace TestPlatform.Database.Seed
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using TestPlatform.Database.Seed.DataSets;
    using TestPlatform.Database.Seed.Interfaces;
    using TestPlatform.Database.Seed.Seeders.Authorization;
    using TestPlatform.Database.Seed.Seeders.Questions;
    using TestPlatform.Database.Seed.Seeders.Subjects;

    public static class ApplicationDbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(TestPlatformDbContext));

            var seeders = new List<ISeeder>
                {
                    new RolesSeeder(serviceProvider, logger, Constants.ROLES_JSON_FILE_NAME),
                    new UserSeeder(serviceProvider, logger, Constants.USERS_JSON_FILE_NAME),
                    new UserRoleMapSeeder(serviceProvider, logger, Constants.USERS_ROLES_MAP_JSON_FILE_NAME),
                    new SubjectTagSeeder(serviceProvider, logger, Constants.SUBJECT_TAGS_JSON_FILE_NAME),
                    new UserSubjectTagMapSeeder(serviceProvider, logger, Constants.USER_SUBJECT_TAG_MAP_JSON_FILE_NAME),
                    new QuestionTypeSeeder(serviceProvider, logger, Constants.QUESTION_TYPE_JSON_FILE_NAME),
                };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }

        public static bool IsNotSeeded(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<TestPlatformDbContext>();

            bool isNotSeeded = dbContext.Roles.Any() == false;

            return isNotSeeded;
        }
    }
}
