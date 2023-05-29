namespace TestPlatform.Services.Database.Test.Interfaces
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestService : IBaseService<Test>
    {
        public Task UpdateSubjectTagsAsync(Guid testId, IEnumerable<Guid> subjectTagsIds, Guid currentUserId);
    }
}
