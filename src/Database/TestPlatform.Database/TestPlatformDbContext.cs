namespace TestPlatform.Database
{
    using Microsoft.EntityFrameworkCore;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Rooms;
    using TestPlatform.Database.Entities.Subjects;
    using TestPlatform.Database.Entities.Tests;

    public class TestPlatformDbContext : DbContext
    {
        public TestPlatformDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoleMap> UsersRolesMap { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionAnswerMap> QuestionsAnswersMap { get; set; }

        public DbSet<QuestionCopy> QuestionCopies { get; set; }

        public DbSet<QuestionTestMap> QuestionsTestsMap { get; set; }

        public DbSet<QuestionType> QuestionTypes { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomParticipantMap> RoomsParticipantsMap { get; set; }

        public DbSet<SubjectTag> SubjectTags { get; set; }

        public DbSet<TestSubjectTagMap> TestsSubjectTagsMap { get; set; }

        public DbSet<UserSubjectTagMap> UsersSubjectTagsMap { get; set; }

        public DbSet<GradeScale> GradeScales { get; set; }

        public DbSet<GradeScaleTestEvaluationMap> GradeScalesTestEvaluationsMap { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestApprovalMap> TestsApprovalsMap { get; set; }

        public DbSet<TestUserMap> TestsUsersMap { get; set; }

        public DbSet<TestEvaluation> TestEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
