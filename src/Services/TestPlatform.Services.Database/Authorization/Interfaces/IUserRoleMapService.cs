namespace TestPlatform.Services.Database.Authorization.Interfaces
{
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Services.Database.Interfaces;

    public interface IUserRoleMapService : IBaseService<UserRoleMap>
    {
        Task<IEnumerable<T>> FindUserRolesAsync<T>(Guid userId);

        Task UpdateUserRolesAsync(Guid userId, IEnumerable<Guid> userRoles, Guid currentUserId);

        Task RemoveRoleFromUserAsync(Guid userRoleMapId);

        Task AddRoleToUserAsync(Guid userId, Guid roleId, Guid currentUserId);
    }
}
