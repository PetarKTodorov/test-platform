namespace TestPlatform.Services.Database.Authorization.Interfaces
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Database.Interfaces;

    public interface IRoleService : IBaseService<Role>
    {
        Task<T> FindByNameAsync<T>(string name);
    }
}
