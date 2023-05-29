namespace TestPlatform.Services.Database.Test.Interfaces
{
    using TestPlatform.Database.Entities.Tests;
    using TestPlatform.Services.Database.Interfaces;

    public interface IStatusService : IBaseService<Status>
    {
        Task<T> FindByNameAsync<T>(string name);
    }
}
