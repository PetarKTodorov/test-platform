namespace TestPlatform.Services.Managers
{
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
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

            //httpContext.Session.Set("Email", Encoding.UTF8.GetBytes(user.Email));
            //httpContext.Session.Set("UserId", Encoding.UTF8.GetBytes(user.Id.ToString()));

            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Email, user.Email),
            //};
            //var identity = new ClaimsIdentity(claims);
            //var principal = new ClaimsPrincipal(identity);

            //httpContext.User = principal;


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, "User"),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddMinutes(10),
            };
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

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