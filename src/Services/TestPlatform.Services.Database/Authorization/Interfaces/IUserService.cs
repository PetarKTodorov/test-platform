namespace TestPlatform.Services.Database.Authorization.Interfaces
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Database.Interfaces;

    public interface IUserService : IBaseService<User>
    {
        Task<T> FindByEmailAndPasswordAsync<T>(string email, string password);

        Task<T> FindByEmailAsync<T>(string email);

        Task<T> FindAllByRoleIdAsync<T>(Guid roleId);
    }
}
