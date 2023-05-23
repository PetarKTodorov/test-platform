namespace TestPlatform.Services.Managers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using TestPlatform.Application.Infrastructures.ApplicationUser;
    using TestPlatform.Common.Constants;
    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.DTOs.BindingModels.User;
    using TestPlatform.DTOs.ViewModels.Users;
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

            var claims = new List<Claim>
            {
                new Claim(UserClaimTypes.ID, user.Id.ToString()),
                new Claim(UserClaimTypes.EMAIL, user.Email),
                new Claim(UserClaimTypes.FULL_NAME, user.FullName),
            };

            var userRoles = await this.userService.FindUserRolesAsync<UserRolesVM>(user.Id);
            userRoles.Roles
                .ToList()
                .ForEach(role => claims.Add(new Claim(UserClaimTypes.ROLE, role.RoleName)));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(claimsPrincipal);

            return true;
        }

        public async Task<bool> RegisterAsync(RegisterUserBM model)
        {
            var registeredUser = await this.userService.FindByEmailAsync<User>(model.Email);

            if (registeredUser != null)
            {
                return false;
            }

            var userTask = this.userService.CreateAsync<User, RegisterUserBM>(model);

            var roleTask = this.roleService.FindByNameAsync<Role>(ApplicationRoles.STUDENT);

            Task.WaitAll(userTask, roleTask);

            var user = userTask.Result;
            var role = roleTask.Result;

            var userRoleMap = new CreateUserRoleMap()
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await this.userRoleMapService.CreateAsync<UserRoleMap, CreateUserRoleMap>(userRoleMap);

            return true;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task UpdateUserRolesAsync(Guid userId, IEnumerable<Guid> userRoles)
        {
            await AddRolesToUserAsync(userId, userRoles);
            await RemoveRolesFromUserAsync(userId, userRoles);
        }

        public async Task RemoveRoleFromUserAsync(Guid userRoleMapId)
        {
            await this.userRoleMapService.HardDeleteAsync<CreateUserRoleMap>(userRoleMapId);
        }

        public async Task AddRoleToUserAsync(Guid userId, Guid roleId)
        {
            var userRoleMap = new CreateUserRoleMap()
            {
                UserId = userId,
                RoleId = roleId,
            };

            await this.userRoleMapService.CreateAsync<CreateUserRoleMap, CreateUserRoleMap>(userRoleMap);
        }

        private async Task AddRolesToUserAsync(Guid userId, IEnumerable<Guid> newRoles)
        {
            var userWithRoles = await this.userService.FindUserRolesAsync<UserRolesVM>(userId);
            var oldUserRolesIds = userWithRoles.Roles.Select(r => r.RoleId);

            var rolesToAdd = newRoles.Where(roleId => !oldUserRolesIds.Contains(roleId));
            foreach (var roleId in rolesToAdd)
            {
                await this.AddRoleToUserAsync(userId, roleId);
            }
        }

        private async Task RemoveRolesFromUserAsync(Guid userId, IEnumerable<Guid> userRoles)
        {
            var userWithRoles = await this.userService.FindUserRolesAsync<UserRolesVM>(userId);
            var oldUserRolesIds = userWithRoles.Roles.Select(r => r.RoleId);

            var rolesToRemove = oldUserRolesIds.Where(roleId => !userRoles.Contains(roleId));
            foreach (var roleId in rolesToRemove)
            {
                var userRoleMapId = userWithRoles.Roles.First(ur => ur.RoleId == roleId);

                await this.RemoveRoleFromUserAsync(userRoleMapId.Id);
            }
        }
    }
}
