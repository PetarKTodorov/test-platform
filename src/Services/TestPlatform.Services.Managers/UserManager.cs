namespace TestPlatform.Services.Managers
{
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.Services.Database.Authorization.Interfaces;
    using TestPlatform.Services.Managers.Interfaces;

    public class UserManager : IUserManager
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserRoleMapService userRoleMapService;

        public UserManager(IUserService userService, IRoleService roleService, IUserRoleMapService userRoleMapService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.userRoleMapService = userRoleMapService;
        }

        public async Task<bool> LoginAsync(LoginUserBM model, HttpContext httpContext)
        {
            var user = await this.userService.FindByEmailAndPasswordAsync<User>(model.Email, model.Password);

            if (user == null)
            {
                return false;
            }

            httpContext.Session.Set("UserEmail", Encoding.UTF8.GetBytes(user.Email));
            httpContext.Session.Set("UserId", Encoding.UTF8.GetBytes(user.Id.ToString()));

            return true;
        }

        public async Task RegisterAsync(RegisterUserBM model)
        {
            var userTask = this.userService.CreateAsync<User, RegisterUserBM>(model);

            var roleTask = this.roleService.GetByNameAsync<Role>(ApplicationRoles.Student.ToString());

            Task.WaitAll(userTask, roleTask);

            var user = userTask.Result;
            var role = roleTask.Result;

            var userRoleMap = new CreateUserRoleMap()
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await this.userRoleMapService.CreateAsync<UserRoleMap, CreateUserRoleMap>(userRoleMap);
        }
    }
}