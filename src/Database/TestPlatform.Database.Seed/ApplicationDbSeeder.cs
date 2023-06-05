namespace TestPlatform.Database.Seed
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using TestPlatform.Database.Seed.DataSets;
    using TestPlatform.Database.Seed.Interfaces;
    using TestPlatform.Database.Seed.Seeders.Authorization;
    using TestPlatform.Database.Seed.Seeders.Questions;
    using TestPlatform.Database.Seed.Seeders.Rooms;
    using TestPlatform.Database.Seed.Seeders.Subjects;
    using TestPlatform.Database.Seed.Seeders.Tests;

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

                    new StatusSeeder(serviceProvider, logger, Constants.STATUS_JSON_FILE_NAME),
                    new TestSeeder(serviceProvider, logger, Constants.TEST_JSON_FILE_NAME),
                    new TestSubjectTagMapSeeder(serviceProvider, logger, Constants.TEST_SUBJECT_TAG_MAP_JSON_FILE_NAME),
                    new TestApprovalMapSeeder(serviceProvider, logger, Constants.TEST_APPROVAL_MAP_FILE_NAME),

                    new QuestionTypeSeeder(serviceProvider, logger, Constants.QUESTION_TYPE_JSON_FILE_NAME),
                    new AnswersSeeder(serviceProvider, logger, Constants.ANSWERS_JSON_FILE_NAME),
                    new QuestionsSeeder(serviceProvider, logger, Constants.QUESTIONS_JSON_FILE_NAME),
                    new QuestionsCopySeeder(serviceProvider, logger, Constants.QUESTIONS_COPY_JSON_FILE_NAME),
                    new QuestionsAnswersMapSeeeder(serviceProvider, logger, Constants.QUESTION_ANSWERS_MAP_JSON_FILE_NAME),
                    new QuestionTestMapSeeder(serviceProvider, logger, Constants.QUESTION_TEST_MAP_JSON_FILE_NAME),

                    new TestEvaluationSeeder(serviceProvider, logger, Constants.TEST_EVALUATION_JSON_FILE_NAME),
                    new GradeScaleSeeder(serviceProvider, logger, Constants.GRADE_SCALE_JSON_FILE_NAME),
                    new GradeScalesTestEvaluationsMapSeeder(serviceProvider, logger, Constants.GRADE_SCALE_TEST_EVALUATION_MAP_JSON_FILE_NAME),

                    new RoomSeeder(serviceProvider, logger, Constants.ROOMS_JSON_FILE_NAME),
                    new RoomParticipantMapSeeder(serviceProvider, logger, Constants.ROOMS_PARTICIPANTS_MAP_JSON_FILE_NAME),
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
