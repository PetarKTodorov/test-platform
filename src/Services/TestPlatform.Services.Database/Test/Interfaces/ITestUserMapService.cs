namespace TestPlatform.Services.Database.Test.Interfaces
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface ITestUserMapService : IBaseService<TestUserMap>
    {
        Task<T> FindByTestIdAndRoomIdAsync<T>(Guid testId, Guid userId);
    }
}
