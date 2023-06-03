namespace TestPlatform.Services.Database.Test.Interfaces
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestUserMapService : IBaseService<TestUserMap>
    {
        IQueryable<T> FindByUserIdAsQueryable<T>(Guid userId);

        Task<T> FindByTestIdAndUserIdAsync<T>(Guid testId, Guid userId);
    }
}
