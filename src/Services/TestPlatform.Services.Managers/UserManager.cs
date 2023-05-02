namespace TestPlatform.Services.Managers
{
    using System.Threading.Tasks;
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

        public async Task LoginAsync(LoginUserBM model)
        {
            throw new NotImplementedException();
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