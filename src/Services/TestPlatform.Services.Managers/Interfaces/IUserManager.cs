namespace TestPlatform.Services.Managers.Interfaces
{
    using Microsoft.AspNetCore.Http;

    using TestPlatform.DTOs.BindingModels.User;

    public interface IUserManager
    {
        Task<bool> RegisterAsync(RegisterUserBM model);

        Task<bool> LoginAsync(LoginUserBM model, HttpContext httpContext);

        Task Logout(HttpContext httpContext);

        Task UpdateUserRolesAsync(Guid userId, IEnumerable<Guid> userRoles, Guid currentUserId);

        Task RemoveRoleFromUserAsync(Guid userRoleMapId);

        Task AddRoleToUserAsync(Guid userId, Guid roleId, Guid currentUserId);
    }
}
